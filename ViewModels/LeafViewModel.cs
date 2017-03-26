using Caliburn.Micro;

namespace MultiselectableTree.ViewModels
{
    public class LeafViewModel : BaseTreeItem
    {
        public LeafViewModel(string title)
            : base(title){}

        public LeafViewModel(string title, IFlatListItem parent)
            : base(title, parent) {}
    }
}
