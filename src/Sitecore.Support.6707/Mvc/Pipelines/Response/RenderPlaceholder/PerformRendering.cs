using Sitecore.Diagnostics;
using Sitecore.Mvc.Exceptions;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Support.Mvc.Pipelines.Response.RenderPlaceholder
{
    public class PerformRendering : Sitecore.Mvc.Pipelines.Response.RenderPlaceholder.PerformRendering
    {
        protected override RecursionStack CreateCyclePreventer(string placeholderName, Rendering rendering)
        {
            Assert.IsNotNull(rendering, "rendering");
            #region Changed code
            string str1 = rendering.UniqueId.ToString(); // Use Unique rendering ID instead of Renderer
            #endregion
            string str2 = placeholderName + "-" + str1;
            string details = "[" + placeholderName + "-" + str1 + "- {" + rendering.UniqueId + "}" + "]";
            RecursionStack recursionStack = new RecursionStack("Rendering", str2, details);
            if (recursionStack.GetCount("Rendering", str2) > 1)
            {
                throw new CyclicRenderingException("A rendering has been recursively embedded within itself. Embedding trail: " + recursionStack.GetTrail("Rendering", " --> "));
            }
            return recursionStack;
        }
    }
}