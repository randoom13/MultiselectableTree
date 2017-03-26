using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace MultiselectableTree.ViewModels
{
    public class TreeOwnerViewModel : PropertyChangedBase
    {
        private readonly BindableCollection<BaseTreeItem> _treeItems = new BindableCollection<BaseTreeItem>();
        public BindableCollection<BaseTreeItem> TreeItems { get { return _treeItems; } }
        public TreeOwnerViewModel()
        {
            FillTree();
            FlatListProvider = new FlatListProvider<BaseTreeItem, CompositeViewModel>(TreeItems);
        }

        public FlatListProvider<BaseTreeItem, CompositeViewModel> FlatListProvider { get; private set; }

        public void OnExpandedChanged(BaseTreeItem item)
        {
            FlatListProvider.OnExpandedChanged(item);
        }

        public void OnSizeChanged(BaseTreeItem item, double actualWidth)
        {
            FlatListProvider.OnSizeChanged(item, actualWidth);
        }

        private void FillTree()
        {
            var topNode = new CompositeViewModel("top node") { IsExpanded = true, };
            var subitems = Enumerable.Range(1, 8).Select(num =>
                {
                    var title = string.Format("node 1.{0} level", num);
                    var composite = new CompositeViewModel(title, topNode)
                    {
                       IsExpanded = true,
                    };
                    if (num % 2 == 0)
                    {
                        title = string.Format("Leaf {0}", num);
                        composite.SubItems.Add(new LeafViewModel(title, composite));
                    }

                    return composite;
                });
            topNode.SubItems.AddRange(subitems);

            TreeItems.Add(topNode);
        }
    }
}
