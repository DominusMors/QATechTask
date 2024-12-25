using System.Collections.Frozen;
using Microsoft.Playwright;

namespace QaTask.Pages;

// TODO: Constraints TLocatorType to be an enum only, TLocatorType is a generic type argument
public abstract class LocatablePageBase<TLocatorType>(IPage page) where TLocatorType : Enum 
{
    protected const string ParamMarker = "{param}";
    protected IPage Page => page;
    protected abstract FrozenDictionary<TLocatorType, string> LocatorTemplates { get; set; }

    public Task NavigateTo(string url) => Page.GotoAsync(url);
    
    protected ILocator GetLocator(TLocatorType locatorType, string? param = null)
    {
        var template = LocatorTemplates[locatorType];

        if (template.Contains(ParamMarker, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(param))
        {
            template = template.Replace(ParamMarker, param);
        }

        return page.Locator(template);
    }
}