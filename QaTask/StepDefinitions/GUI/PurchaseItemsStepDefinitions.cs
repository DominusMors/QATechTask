using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using QaTask.Contracts.Interfaces;
using TechTalk.SpecFlow;

namespace QaTask.StepDefinitions.GUI;

[Binding]
public class PurchaseItemsStepDefinitions(IAppConfiguration appConfiguration, IInventoryPage inventoryPage, ScenarioContext scenarioContext)
{
    private const string ContextTotalPriceKey = "TotalPrice";

    [Given(@"i add an item to my cart to purchase")]
    public Task AddItemToCart() 
        => inventoryPage.AddBackPackToCartButton();

    [When(@"i navigate to the cart to checkout my purchase")]
    public async Task NavigateToCart()
    {
        await inventoryPage.ClickShopCartButton();
        await inventoryPage.ClickCheckoutButton();
    }

    [StepDefinition(@"i complete my personal information")]
    public async Task CompletePersonalInformation()
    {
        await inventoryPage.EnterFirstName(appConfiguration.FirstName);
        await inventoryPage.EnterLastName(appConfiguration.LastName);
        await inventoryPage.EnterPostCode(appConfiguration.PostalCode);
        await inventoryPage.ClickContinueButton();
    }

    [When(@"overview my order")]
    public async Task OverviewOrder()
    {
        var cartContent = await inventoryPage.CartItem();
        cartContent.Should().BeEquivalentTo("1");
    }

    [Then(@"i can assert that my order has been placed")]
    public async Task OrderHasBeenPlaced()
    {
        await inventoryPage.CompletePurchaseButton();
        
        var orderCompletion = await inventoryPage.OrderCompletionText();
        orderCompletion.Should().BeEquivalentTo("Thank you for your order!");
    }

    [When(@"I skip filling in my personal information")]
    public Task SkipFillingPersonalInfo() 
        => inventoryPage.ClickContinueButton();


    [Then(@"I should see error messages indicating that personal information is required for each field")]
    public async Task ErrorMessagesForPersonalInformation()
    {
        using (new AssertionScope())
        {
            var errorMessage = await inventoryPage.InformationErrorMessage();
            errorMessage.Should().BeEquivalentTo(appConfiguration.FirstNameError);
            await inventoryPage.EnterFirstName(appConfiguration.FirstNameError);

            await SkipFillingPersonalInfo();
            errorMessage = await inventoryPage.InformationErrorMessage();
            errorMessage.Should().BeEquivalentTo(appConfiguration.LastNameError);

            await inventoryPage.EnterLastName(appConfiguration.LastNameError);
            await SkipFillingPersonalInfo();

            errorMessage = await inventoryPage.InformationErrorMessage();
            errorMessage.Should().BeEquivalentTo(appConfiguration.PostalCodeError);
        }
    }

    [Given(@"I add multiple items to my cart")]
    public async Task AddmultipleItemsToCart()
    {
        await inventoryPage.AddBackPackToCartButton();
        await inventoryPage.AddFleeceJacketToCartButton();
        await inventoryPage.AddTshirtToCartButton();

        var priceBackpackString = await inventoryPage.GetItemPrice(appConfiguration.BackPackPrice);
        var priceFleeceJacketString = await inventoryPage.GetItemPrice(appConfiguration.FleeceJacketPrice);
        var priceTShirtString = await inventoryPage.GetItemPrice(appConfiguration.TshirtPrice);

        priceBackpackString.Should().NotBeNullOrEmpty();
        priceFleeceJacketString.Should().NotBeNullOrEmpty();
        priceTShirtString.Should().NotBeNullOrEmpty();

        var canParsePriceBackpack = decimal.TryParse(priceBackpackString!.Trim('$'), out var priceBackpack);
        canParsePriceBackpack.Should().BeTrue();

        var canParsePriceFleeceJacket = decimal.TryParse(priceFleeceJacketString!.Trim('$'), out var priceFleeceJacket);
        canParsePriceFleeceJacket.Should().BeTrue();

        var canParsePriceTShirt = decimal.TryParse(priceTShirtString!.Trim('$'), out var priceTshirt);
        canParsePriceTShirt.Should().BeTrue();

        var totalPrice = priceBackpack + priceFleeceJacket + priceTshirt;
        scenarioContext.Set(totalPrice, ContextTotalPriceKey);
    }

    [Then(@"I can assert that the total price matches the sum of the items' prices")]
    public async Task AssertTotalPriceisCorrect()
    {
        var priceWithoutTax = await inventoryPage.TotalPriceWithoutTax();
        priceWithoutTax.Should().NotBeNullOrEmpty();

        var canParsePrice = decimal.TryParse(priceWithoutTax!.Replace("Item total: $", ""), out var price);
        canParsePrice.Should().BeTrue(because: "Price should be a valid decimal");

        var itemsPrice = scenarioContext.Get<decimal>(ContextTotalPriceKey);
        price.Should().Be(itemsPrice);
    }
}