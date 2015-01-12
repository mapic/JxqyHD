﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Engine.Gui.Base
{
    public class ListItem : GuiItem
    {
        #region Event
        public event Action<object, ListItemClickEvent> ItemClick;
        #endregion Event

        public ListItem() { }

        public ListItem(
            GuiItem parent,
            Vector2 position,
            int width,
            int height,
            Texture baseTexture
            )
            :base(parent, position, width, height, baseTexture)
        {
            
        }

        #region Event method

        protected virtual void OnItemClick(ListItemClickEvent e)
        {
            if (ItemClick != null)
            {
                ItemClick(this, e);
            }
        }
        #endregion 

        #region event class

        public class ListItemEvent : EventArgs
        {
            public int ItemIndex { private set; get; }
            public ListItemEvent(int itemIndex)
            {
                ItemIndex = itemIndex;
            }
        }

        public class ListItemClickEvent: ListItemEvent
        {
            public ListItemClickEvent(int itemIndex)
                :base(itemIndex)
            {
                
            }
        }
        #endregion
    }
}