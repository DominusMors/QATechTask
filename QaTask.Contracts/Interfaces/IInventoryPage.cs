namespace QaTask.Contracts.Interfaces;

public interface IInventoryPage
{
    Task OpenBurgerMenu();
    Task ClickLogoutButton();
    Task AddBackPackToCartButton();
    Task AddFleeceJacketToCartButton();
    Task AddTshirtToCartButton();
    Task ClickShopCartButton();
    Task ClickCheckoutButton();
    Task EnterFirstName(string firstName);
    Task EnterLastName(string lastName);
    Task EnterPostCode(string postCode);
    Task ClickContinueButton();
    Task<string?> CartItem();
    Task CompletePurchaseButton();
    Task<string?> OrderCompletionText();
    Task<string?> InformationErrorMessage();
    Task<string?> GetItemPrice(string itemName);
    Task<string?> TotalPriceWithoutTax();

}