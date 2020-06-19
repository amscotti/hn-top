using CommandLine;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace hn_top
{


    class Program
    {

        public class Options
        {
            [Option('n', "number", Required = false, Default = 5, HelpText = "Number of top news to show.")]
            public int Number { get; set; }

            [Option('u', "source_urls", Required = false, Default = false, HelpText = "Show source urls.")]
            public bool SourceUrls { get; set; }
        }

        static void PrintStory(Story s, bool showSource)
        {
            Console.WriteLine($"{s.Title}");
            Console.WriteLine($"score: {s.Score}\tcomments: {s.Descendants}\tuser: {s.By}");
            Console.WriteLine($"url: https://news.ycombinator.com/item?id={s.Id}");
            if (showSource)
            {
                Console.WriteLine($"{s.URL}");
            }
            Console.WriteLine("");
        }

        static async Task Load(Options options)
        {
            var service = new StoryService();
            Console.WriteLine("Fetching latest stories...\n");
            var topStories = await service.GetTopStories();

            var storyTaskList = topStories.Take(options.Number).Select(service.GetStory);
            var stories = await Task.WhenAll(storyTaskList);

            foreach (Story s in stories)
            {
                PrintStory(s, options.SourceUrls);
            }

        }

        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<Options>(args).WithParsedAsync(Load);
        }
    }
}
