using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace MultiselectableTree.ViewModels
{
    public class LevelInfo
    {
        public List<IFlatListItem> Items { get; private set; }
        public LevelInfo(List<IFlatListItem> items)
        {
            Items = items;
        }
    }

    public class FlatListProvider<Node, Parent>
        where Node : class, IFlatListItem
         where Parent : class, ICompositeFlatListItem<Node>
    {
        protected readonly BindableCollection<Node> _treeItems;
        protected readonly List<LevelInfo> _levelInfos = new List<LevelInfo>();

        public FlatListProvider(BindableCollection<Node> treeItems)
        {
            _treeItems = treeItems;
            RefreshFlatList();
            _treeItems.CollectionChanged += CollectionChangedHandler;
        }

        private void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
                RefreshFlatList();
        }

        public void OnExpandedChanged(Node child)
        {
                RefreshFlatList();
        }

        public void OnSizeChanged(Node node, double actualWidth)
        {
                ModifyItemsSize(node, actualWidth);
        }

        private void ModifyItemsSize(Node node, double width)
        {
            if (node == null)
                return;
            var levelInfo = _levelInfos.ElementAtOrDefault(node.Level);
            if (levelInfo == null)
                return;

            if (node.SizeWidth != width)
                node.SizeWidth = width;

            var nextLevel = node.Level + 1;
            if (nextLevel < _levelInfos.Count)
            {
                var maxSizeWidth = levelInfo.Items.Max(it => it.SizeWidth);
                _levelInfos[nextLevel].Items.ForEach(
                    it =>
                    {
                        if (it.LeftMargin != maxSizeWidth)
                            it.LeftMargin = maxSizeWidth;
                    });
            }

        }

        private List<IFlatListItem> ParseTreeToFlatList()
        {
            var newItems = new List<IFlatListItem>();
            newItems.AddRange(_treeItems.SelectMany(ti => ti.ToFlatList()));
            return newItems;
        }

        protected void RefreshFlatList()
        {
            foreach (var oldParent in Items.OfType<Parent>())
                oldParent.SubItems.CollectionChanged -= CollectionChangedHandler;
            
            Items.IsNotifying = false;
            Items.Clear();
            var newItems = ParseTreeToFlatList();
            Items.AddRange(newItems);
            Items.IsNotifying = true;
            RefreshLevelInfos();
            foreach (var newParent in Items.OfType<Parent>())
                newParent.SubItems.CollectionChanged += CollectionChangedHandler;

            Items.Refresh();
        }

        private void RefreshLevelInfos()
        {
            _levelInfos.Clear();
            var newlevelInfos = Items.GroupBy(i => i.Level)
                                .Select(i => new 
                                {
                                    Items = i.Where(it => it.Level == i.Key).ToList(),
                                    Level = i.Key
                                }).OrderBy(it=>it.Level);

            _levelInfos.AddRange(newlevelInfos.Select(it=> new LevelInfo(it.Items)));
        }
        private readonly BindableCollection<IFlatListItem> _items =
            new BindableCollection<IFlatListItem>();
        public BindableCollection<IFlatListItem> Items
        {
            get { return _items; }
        }
    }
}
