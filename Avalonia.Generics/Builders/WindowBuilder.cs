using Avalonia.Controls;
using Avalonia.Generics.Controls;
using Avalonia.Media;
using Avalonia.MenuFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Generics.Builders
{
    public class WindowBuilder
    {
        private readonly Dictionary<string, object> Properties = new() {
            { "canResize", true },
            { "canMinimize", true },
            { "maxWidth", double.NaN },
            { "maxHeight", double.NaN }
        };

        /// <summary>
        /// Adds a <see cref="string"/> Title to the current <see cref="WindowBuilder"/>
        /// </summary>
        /// <param name="title"></param>
        public WindowBuilder WithTitle(string title)
        {
            Properties[nameof(title)] = title;
            return this;
        }

        /// <summary>
        /// Adds an <see cref="IImage"/> icon to the current <see cref="WindowBuilder"/>
        /// </summary>
        /// <param name="icon"></param>
        public WindowBuilder WithIcon(IImage icon)
        {
            Properties[nameof(icon)] = icon;
            return this;
        }

        /// <summary>
        /// Sets the current <see cref="WindowBuilder"/> contents
        /// </summary>
        /// <param name="content"></param>
        public WindowBuilder WithContent(object content)
        {
            Properties[nameof(content)] = content;
            return this;
        }

        /// <summary>
        /// Adds a Menu object to the current <see cref="WindowBuilder"/> to be parsed by the <see cref="MenuFactory.MenuFactory"/> default implementation
        /// </summary>
        /// <param name="menuFactory">A class instance defining the <see cref="MenuItem"/>s as <see cref="MenuFactory.Attributes.MenuAttribute"/> attributed methods</param>
        public WindowBuilder WithMenu(object menuFactory)
        {
            Properties[nameof(menuFactory)] = menuFactory;
            return this;
        }

        /// <summary>
        /// <!-- Adds a Menu object to the current <see cref="WindowBuilder"/> to be parsed by the <see cref="MenuFactory.MenuFactory"/> default implementation -->
        /// </summary>
        /// <param name="title"></param>
        public WindowBuilder WithChromeBar(object chromeFactory)
        {
            Properties[nameof(chromeFactory)] = chromeFactory;
            return this;
        }

        /// <summary>
        /// Adds a minimum With and Height to the current <see cref="WindowBuilder"/>
        /// </summary>
        /// <param name="minWidth"></param>
        /// <param name="minHeight"></param>
        public WindowBuilder WithMinBounds(double minWidth, double minHeight)
        {
            Properties[nameof(minWidth)] = minWidth;
            Properties[nameof(minHeight)] = minHeight;
            return this;
        }

        /// <summary>
        /// Adds a maximum With and Height to the current <see cref="WindowBuilder"/>
        /// </summary>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        public WindowBuilder WithMaxBounds(double maxWidth, double maxHeight)
        {
            Properties[nameof(maxWidth)] = maxWidth;
            Properties[nameof(maxHeight)] = maxHeight;
            return this;
        }

        /// <summary>
        /// Adds window resize rules to the current <see cref="WindowBuilder"/>
        /// </summary>
        /// <param name="canResize"></param>
        /// <param name="canMinimize"></param>
        public WindowBuilder WithResizeRules(bool canResize = false, bool canMinimize = true)
        {
            Properties[nameof(canResize)] = canResize;
            Properties[nameof(canMinimize)] = canMinimize;
            return this;
        }

        /// <summary>
        /// Builds the current <see cref="WindowBuilder"/> into a <see cref="GenericWindow"/> control
        /// </summary>
        /// <returns></returns>
        public Window Build()
        {
            return new GenericWindow(
                Get<string>("title"),
                Get<object>("content"),
                Get<object>("menuFactory"),
                Get<object>("chromeFactory"),
                Get<bool>("canResize"),
                Get<bool>("canMinimize"),
                Get<double>("minWidth"),
                Get<double>("minHeight"),
                Get<double>("maxWidth"),
                Get<double>("maxHeight"),
                Get<IImage>("icon")
            );
        }

        internal T? Get<T>(string key)
        {
            if (Properties.ContainsKey(key)) {
                return (T)Properties[key];
            }
            else {
                return default;
            }
        }
    }
}
