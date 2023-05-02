using System;

namespace UI
{
    public class LostPanel: PanelBase
    {
        public override event Action ClickedPanel;
        protected override void OnClickedPanel()
        {
            ClickedPanel?.Invoke();
        }
    }
}