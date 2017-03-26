using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;

namespace MultiselectableTree.ViewModels
{
    public interface IFlatListItem
    {
        IFlatListItem Parent { get; set; }
        int Level { get; }
        double SizeWidth { get; set; }
        double LeftMargin { get; set; }
        IEnumerable<IFlatListItem> ToFlatList();
        void AttachTo(IFlatListItem parent);
        bool IsSelected { get; set; }
    }

    public interface ICompositeFlatListItem<BaseNode> : IFlatListItem
                     where BaseNode : class, IFlatListItem
    {
        bool IsExpanded { get; set; }
        BindableCollection<BaseNode> SubItems { get; }
    }

}
