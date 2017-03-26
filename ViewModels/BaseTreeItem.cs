using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MultiselectableTree.ViewModels
{
    public abstract class BaseTreeItem : PropertyChangedBase, IFlatListItem
    {
        private string _title;
        public string Title 
        {
            get { return _title; }
            set { _title = value; NotifyOfPropertyChange(() => Title); }
        }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; NotifyOfPropertyChange(() => IsSelected); }
        }
        public BaseTreeItem(string title, IFlatListItem parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            Title = title;
            AttachTo(parent);
        }

        public BaseTreeItem(string title)
        {
            Title = title;
        }

        public IFlatListItem Parent { get; set; }
        public double SizeWidth { get; set; }
        public int Level
        {
            get { return Parent != null ? Parent.Level + 1 : 0; }
        }

        private double _leftMargin;
        public double LeftMargin
        {
            get { return _leftMargin; }
            set
            {
                _leftMargin = value;
                NotifyOfPropertyChange(() => LeftMargin);
            }
        }

        public virtual IEnumerable<IFlatListItem> ToFlatList()
        {
            yield return this;
        }

        public void AttachTo(IFlatListItem parent)
        {
            Parent = parent;
            LeftMargin = parent == null ? 0 : parent.SizeWidth;
        }
    }
}
