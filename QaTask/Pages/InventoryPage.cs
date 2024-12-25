using System.Collections.Frozen;
using Microsoft.Playwright;
using QaTask.Contracts.Enums;
using QaTask.Contracts.Interfaces;

namespace QaTask.Pages;

public class InventoryPage(IPage page) : LocatablePageBase<InventoryPageLocators>(page), IInventoryPage
{
    // Frozen dictionary is readonly, has a little performance hit on constructions and is extremely fast on reads
    protected override FrozenDictionary<InventoryPageLocators, string> LocatorTemplates { get; set; } =
        new Dictionary<InventoryPageLocators, string>()
        {
            [InventoryPageLocators.BurgerMenuButton] = "//div[@class='bm-burger-button']",
            [InventoryPageLocators.LogoutButton] = "//a[@id='logout_sidebar_link']",
            [InventoryPageLocators.AddBackPackItemToCart] = "//button[@id='add-to-cart-sauce-labs-backpack']",
            [InventoryPageLocators.AddFleeceJacketToCart] = "//button[@id='add-to-cart-sauce-labs-fleece-jacket']",
            [InventoryPageLocators.AddTshirtToCart] = "//button[@id='add-to-cart-sauce-labs-bolt-t-shirt']",
            [InventoryPageLocators.ShopCartButton] = "//div[@id='shopping_cart_container']",
            [InventoryPageLocators.CheckoutButton] = "//button[@id='checkout']",
            [InventoryPageLocators.FirstNameField] = "//input[@id='first-name']",
            [InventoryPageLocators.LastNameField] = "//input[@id='last-name']",
            [InventoryPageLocators.PostcodeField] = "//input[@id='postal-code']",
            [InventoryPageLocators.ContinueButton] = "//input[@id='continue']",
            [InventoryPageLocators.CartItemQuantity] = "//div[@class='cart_quantity']",
            [InventoryPageLocators.FinishButton] = "//button[@id='finish']",
            [InventoryPageLocators.OrderCompletion] = "//h2[@class='complete-header']",
            [InventoryPageLocators.ErrorMessage] = "//div[@class='error-message-container error']",
            [InventoryPageLocators.ItemPrice] = $".inventory_item:has-text('{ParamMarker}') .inventory_item_price",
            [InventoryPageLocators.GetTotalPriceWithoutTax] = "//div[@class='summary_subtotal_label']"
        }.ToFrozenDictionary();

    public Task OpenBurgerMenu() => GetLocator(InventoryPageLocators.BurgerMenuButton).ClickAsync();
    public Task ClickLogoutButton() => GetLocator(InventoryPageLocators.LogoutButton).ClickAsync();
    public Task AddBackPackToCartButton() => GetLocator(InventoryPageLocators.AddBackPackItemToCart).ClickAsync();
    public Task AddFleeceJacketToCartButton() => GetLocator(InventoryPageLocators.AddFleeceJacketToCart).ClickAsync();
    public Task AddTshirtToCartButton() => GetLocator(InventoryPageLocators.AddTshirtToCart).ClickAsync();
    public Task ClickShopCartButton() => GetLocator(InventoryPageLocators.ShopCartButton).ClickAsync();
    public Task ClickCheckoutButton() => GetLocator(InventoryPageLocators.CheckoutButton).ClickAsync();
    public Task EnterFirstName(string firstName) => GetLocator(InventoryPageLocators.FirstNameField).FillAsync(firstName);
    public Task EnterLastName(string lastName) => GetLocator(InventoryPageLocators.LastNameField).FillAsync(lastName);
    public Task EnterPostCode(string postCode) => GetLocator(InventoryPageLocators.PostcodeField).FillAsync(postCode);
    public Task ClickContinueButton() => GetLocator(InventoryPageLocators.ContinueButton).ClickAsync();
    public Task<string?> CartItem() => GetLocator(InventoryPageLocators.CartItemQuantity).TextContentAsync();
    public Task CompletePurchaseButton() => GetLocator(InventoryPageLocators.FinishButton).ClickAsync();
    public Task<string?> OrderCompletionText() => GetLocator(InventoryPageLocators.OrderCompletion).TextContentAsync();
    public Task<string?> InformationErrorMessage() => GetLocator(InventoryPageLocators.ErrorMessage).TextContentAsync();
    public Task<string?> GetItemPrice(string itemName) => GetLocator(InventoryPageLocators.ItemPrice, itemName).TextContentAsync();
    public Task<string?> TotalPriceWithoutTax() => GetLocator(InventoryPageLocators.GetTotalPriceWithoutTax).TextContentAsync();
}