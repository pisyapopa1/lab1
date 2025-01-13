using Microsoft.AspNetCore.Mvc;

namespace lab1.Components
{
    public class RecentArticlesViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = new List<string>
            {
                "Стаття 1",
                "Стаття 2",
                "Стаття 3",
                "Стаття 4",
                "Стаття 5"
            };

            return await Task.FromResult(View(articles));
        }
    }
}
