﻿using System;

namespace UI
{
    public class StartMenu : PanelBase
    {
        public override event Action ClickedPanel;
        protected override void OnClickedPanel()
        {
           ClickedPanel?.Invoke();
        }
    }
}