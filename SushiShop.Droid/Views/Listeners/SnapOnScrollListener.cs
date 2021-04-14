using System;
using AndroidX.RecyclerView.Widget;
using SushiShop.Droid.Enums;
using static AndroidX.RecyclerView.Widget.RecyclerView;

namespace SushiShop.Droid.Views.Listeners
{
    public class SnapOnScrollListener : OnScrollListener
    {
        private readonly SnapHelper snapHelper;
        private readonly BehaviorScrollListener behavior;
        private readonly Action<int> positionChangedAction;

        private bool isInternal;

        public SnapOnScrollListener(SnapHelper snapHelper, BehaviorScrollListener behavior, Action<int> onPositionChanged)
        {
            this.snapHelper = snapHelper;
            this.behavior = behavior;
            positionChangedAction = onPositionChanged;
        }

        private int position = -1;
        public int Position
        {
            get => position;
            set
            {
                position = value;
                if (!isInternal)
                {
                    return;
                }

                positionChangedAction(value);
            }
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            if (behavior == BehaviorScrollListener.NotifyOnScroll)
            {
                FindNewPosition(recyclerView);
            }
        }

        public override void OnScrollStateChanged(RecyclerView recyclerView, int newState)
        {
            if (behavior == BehaviorScrollListener.NotifyOnScrollStateIdle && newState == RecyclerView.ScrollStateIdle)
            {
                FindNewPosition(recyclerView);
            }
        }

        private void FindNewPosition(RecyclerView recyclerView)
        {
            var position = snapHelper.FindTargetSnapPosition(recyclerView.GetLayoutManager(), recyclerView.ScrollX, recyclerView.ScrollY);
            var snapPositionChanged = position != Position;
            if (snapPositionChanged)
            {
                isInternal = true;
                Position = position;
                isInternal = false;
            }
        }
    }
}
