﻿using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Engine.Gui.Base;
using Engine.ListManager;
using IniParser;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Texture = Engine.Gui.Base.Texture;

namespace Engine.Gui
{
    public static class GuiManager
    {
        private static SoundEffect _dropSound;
        private static SoundEffect _interfaceShow;
        private static SoundEffect _interfaceMiss;
        private static LinkedList<GuiItem> _allGuiItems = new LinkedList<GuiItem>();
        private static LinkedList<GuiItem> _panels = new LinkedList<GuiItem>();
        private static KeyboardState _lastKeyboardState;
        private static MouseState _lastMouseState;

        public static TitleGui TitleInterface;
        public static SaveLoadGui SaveLoadInterface;
        public static MagicGui MagicInterface;
        public static XiuLianGui XiuLianInterface;
        public static GoodsGui GoodsInterface;
        public static MemoGui MemoInterface;
        public static StateGui StateInterface;
        public static EquipGui EquipInterface;
        public static BottomGui BottomInterface;
        public static ColumnGui ColumnInterface;
        public static TopGui TopInterface;
        public static BuyGui BuyInterface;
        public static TimerGui TimerInterface;

        public static ToolTipGui ToolTipInterface;
        public static MessageGui MessageInterface;
        public static DialogGui DialogInterface;
        public static MouseGui MouseInterface;

        public static bool IsMouseStateEated;
        public static DragDropItem DragDropSourceItem;
        public static Texture DragDropSourceTexture;
        public static bool IsDropped;

        public static void Starting()
        {
            _dropSound = Utils.GetSoundEffect("界-拖放.wav");
            _interfaceShow = Utils.GetSoundEffect("界-弹出菜单.wav");
            _interfaceMiss = Utils.GetSoundEffect("界-缩回菜单.wav");

            TitleInterface = new TitleGui();
            _allGuiItems.AddLast(TitleInterface);

            SaveLoadInterface = new SaveLoadGui();
            _allGuiItems.AddLast(SaveLoadInterface);

            TopInterface = new TopGui();
            _allGuiItems.AddLast(TopInterface);

            BottomInterface = new BottomGui();
            _allGuiItems.AddLast(BottomInterface);

            ColumnInterface = new ColumnGui();
            _allGuiItems.AddLast(ColumnInterface);

            MagicInterface = new MagicGui();
            _allGuiItems.AddLast(MagicInterface);
            _panels.AddLast(MagicInterface);

            XiuLianInterface = new XiuLianGui();
            _allGuiItems.AddLast(XiuLianInterface);
            _panels.AddLast(XiuLianInterface);

            GoodsInterface = new GoodsGui();
            _allGuiItems.AddLast(GoodsInterface);
            _panels.AddLast(GoodsInterface);

            BuyInterface = new BuyGui();
            _allGuiItems.AddLast(BuyInterface);
            _panels.AddLast(BuyInterface);

            TimerInterface = new TimerGui();
            _allGuiItems.AddLast(TimerInterface);

            MemoInterface = new MemoGui();
            _allGuiItems.AddLast(MemoInterface);
            _panels.AddLast(MemoInterface);

            StateInterface = new StateGui();
            _allGuiItems.AddLast(StateInterface);
            _panels.AddLast(StateInterface);

            EquipInterface = new EquipGui();
            _allGuiItems.AddLast(EquipInterface);
            _panels.AddLast(EquipInterface);

            ToolTipInterface = new ToolTipGui();
            _allGuiItems.AddLast(ToolTipInterface);
            _panels.AddLast(ToolTipInterface);

            MouseInterface = new MouseGui();

            MessageInterface = new MessageGui();
            _allGuiItems.AddLast(MessageInterface);
            _panels.AddLast(MessageInterface);

            DialogInterface = new DialogGui();
            _allGuiItems.AddLast(DialogInterface);

            MagicListManager.RenewList();
            GoodsListManager.RenewList();
        }

        public static void Load(string magicListPath, string goodsListPath, string memoListPath)
        {
            MagicListManager.LoadList(magicListPath);
            GoodsListManager.LoadList(goodsListPath);
            MemoListManager.LoadList(memoListPath);
        }

        public static void Save(string magicListPath, string goodsListPath, string memoListPath)
        {
            MagicListManager.SaveList(magicListPath);
            GoodsListManager.SaveList(goodsListPath);
            MemoListManager.SaveList(memoListPath);
        }

        public static void PlayInterfaceShowMissSound(bool isShow)
        {
            if (isShow) _interfaceMiss.Play();
            else _interfaceShow.Play();
        }

        public static void ToggleMagicGuiShow()
        {
            PlayInterfaceShowMissSound(MagicInterface.IsShow);
            if (MagicInterface.IsShow)
                MagicInterface.IsShow = false;
            else
            {
                MagicInterface.IsShow = true;
                GoodsInterface.IsShow = false;
                MemoInterface.IsShow = false;
            }
        }

        public static void ToggleGoodsGuiShow()
        {
            PlayInterfaceShowMissSound(GoodsInterface.IsShow);
            if (GoodsInterface.IsShow)
                GoodsInterface.IsShow = false;
            else
            {
                GoodsInterface.IsShow = true;
                MagicInterface.IsShow = false;
                MemoInterface.IsShow = false;
            }
        }

        public static void ToggleMemoGuiShow()
        {
            PlayInterfaceShowMissSound(MemoInterface.IsShow);
            if (MemoInterface.IsShow)
                MemoInterface.IsShow = false;
            else
            {
                MemoInterface.IsShow = true;
                GoodsInterface.IsShow = false;
                MagicInterface.IsShow = false;
            }
        }

        public static void ToggleXiuLianGuiShow()
        {
            PlayInterfaceShowMissSound(XiuLianInterface.IsShow);
            if (XiuLianInterface.IsShow)
                XiuLianInterface.IsShow = false;
            else
            {
                XiuLianInterface.IsShow = true;
                StateInterface.IsShow = false;
                EquipInterface.IsShow = false;
            }
        }

        public static void ToggleStateGuiShow()
        {
            PlayInterfaceShowMissSound(StateInterface.IsShow);
            if (StateInterface.IsShow)
                StateInterface.IsShow = false;
            else
            {
                StateInterface.IsShow = true;
                XiuLianInterface.IsShow = false;
                EquipInterface.IsShow = false;
            }
        }

        public static void ToggleEquipGuiShow()
        {
            PlayInterfaceShowMissSound(StateInterface.IsShow);
            if (EquipInterface.IsShow)
                EquipInterface.IsShow = false;
            else
            {
                EquipInterface.IsShow = true;
                XiuLianInterface.IsShow = false;
                StateInterface.IsShow = false;
            }
        }

        public static void UpdateMagicView()
        {
            MagicInterface.UpdateItems();
            BottomInterface.UpdateMagicItems();
            XiuLianInterface.UpdateItem();
        }

        public static void UpdateGoodsView()
        {
            GoodsInterface.UpdateItems();
            BottomInterface.UpdateGoodsItems();
            EquipInterface.UpdateItems();
        }

        public static void UpdateGoodItemView(int listIndex)
        {
            if (GoodsListManager.IsInStoreRange(listIndex))
            {
                GoodsInterface.UpdateListItem(listIndex);
            }
            else if (GoodsListManager.IsInBottomGoodsRange(listIndex))
            {
                BottomInterface.UpdateGoodItem(listIndex);
            }
        }

        public static void UpdateMemoView()
        {
            MemoInterface.UpdateTextShow();
        }

        public static void Show(bool isShow = true)
        {
            foreach (var item in _allGuiItems)
            {
                item.IsShow = isShow;
            }
        }

        public static void AllPanelsShow(bool show = true)
        {
            foreach (var panel in _panels)
            {
                panel.IsShow = false;
            }
        }

        public static bool HasPanelsShow()
        {
            foreach (var panel in _panels)
            {
                if (panel.IsShow) return true;
            }
            return false;
        }

        #region Functionail method

        public static void ShowTitle(bool isShow = true)
        {
            TitleInterface.IsShow = isShow;
        }

        private static void ShowSaveLoad(bool isShow, bool canSave)
        {
            SaveLoadInterface.IsShow = isShow;
            SaveLoadInterface.CanSave = canSave;
        }

        public static void ShowSaveLoad(bool isShow = true)
        {
            ShowSaveLoad(isShow, true);
        }

        public static void ShowLoad(bool isShow = true)
        {
            ShowSaveLoad(isShow, false);
        }

        public static void ShowMessage(string message)
        {
            MessageInterface.ShowMessage(message);
        }

        public static void ShowDialog(string text, int portraitIndex = -1)
        {
            AllPanelsShow(false);
            DialogInterface.ShowText(text, portraitIndex);
        }

        public static void Selection(string message, string selectA, string selectionB)
        {
            AllPanelsShow(false);
            DialogInterface.Select(message, selectA, selectionB);
        }

        public static bool IsSelectionEnd()
        {
            return !DialogInterface.IsInSelecting;
        }

        public static int GetSelection()
        {
            return DialogInterface.Selection;
        }

        public static bool IsDialogEnd()
        {
            return !DialogInterface.IsShow;
        }

        public static void AddMemo(string text)
        {
            MemoListManager.AddMemo(text);
            UpdateMemoView();
        }

        public static void DeleteGood(string fileName)
        {
            GoodsListManager.DeleteGood(fileName);
            UpdateGoodsView();
        }

        public static void EquipGoods(int goodListIndex, Good.EquipPosition part)
        {
            if (!GoodsListManager.CanEquip(goodListIndex, part)) return;
            EquipInterface.EquipGood(goodListIndex);
        }

        public static void BuyGoods(string listFileName)
        {
            AllPanelsShow(false);
            BuyInterface.BeginBuy(listFileName);
            GoodsInterface.IsShow = true;
        }

        public static void EndBuyGoods()
        {
            BuyInterface.IsShow = false;
            GoodsInterface.IsShow = false;
        }

        public static bool IsBuyGoodsEnd()
        {
            return !BuyInterface.IsShow;
        }

        public static void OpenTimeLimit(int seconds)
        {
            TimerInterface.StartTimer(seconds);
        }

        public static void CloseTimeLimit()
        {
            TimerInterface.StopTimer();
        }

        public static void HideTimerWindow()
        {
            TimerInterface.HideTimerWnd();
        }

        public static int GetTimerCurrentSeconds()
        {
            return TimerInterface.GetCurrentSecond();
        }

        public static bool IsTimerStarted()
        {
            return TimerInterface.IsShow;
        }

        #endregion Functionail method

        public static void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            MouseInterface.Update(gameTime);
            ColumnInterface.Update(gameTime);

            //check mouse state
            if (
                IsMouseStateEated &&
                    (
                    mouseState.LeftButton == ButtonState.Pressed ||
                    mouseState.RightButton == ButtonState.Pressed ||
                    mouseState.MiddleButton == ButtonState.Pressed
                    )
                )
            {
                IsMouseStateEated = true;
            }
            else IsMouseStateEated = false;

            if (SaveLoadInterface.IsShow)
            {
                //Temporaty enable input
                Globals.EnableInputTemporary();

                SaveLoadInterface.Update(gameTime);

                //Restore input
                Globals.RestoreInputDisableState();
            }
            else if (TitleInterface.IsShow)
            {
                TitleInterface.Update(gameTime);
            }
            else if (DialogInterface.IsShow)
            {
                //Temporaty enable input
                Globals.EnableInputTemporary();

                IsMouseStateEated = true;
                if (DialogInterface.IsInSelecting)
                {
                    DialogInterface.Update(gameTime);
                    //Check wheathe selection ended after updated
                    if (!DialogInterface.IsInSelecting)
                    {
                        DialogInterface.IsShow = false;
                    }
                }
                else
                {
                    DialogInterface.Update(gameTime);
                    if (mouseState.LeftButton == ButtonState.Pressed &&
                    _lastMouseState.LeftButton == ButtonState.Released)
                    {
                        if (!DialogInterface.NextPage())
                            DialogInterface.IsShow = false;
                    }
                }
                //Restore input
                Globals.RestoreInputDisableState();
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Escape) &&
                    _lastKeyboardState.IsKeyUp(Keys.Escape))
                {
                    if (HasPanelsShow()) AllPanelsShow(false);
                }

                if (BuyInterface.IsShow)
                {
                    //Temporaty enable input
                    Globals.EnableInputTemporary();

                    BuyInterface.Update(gameTime);
                    GoodsInterface.Update(gameTime);
                    BottomInterface.Update(gameTime);
                    IsMouseStateEated = true;

                    //Restore input
                    Globals.RestoreInputDisableState();
                }
                else
                {
                    TopInterface.Update(gameTime);
                    BottomInterface.Update(gameTime);
                    MagicInterface.Update(gameTime);
                    XiuLianInterface.Update(gameTime);
                    GoodsInterface.Update(gameTime);
                    MemoInterface.Update(gameTime);
                    StateInterface.Update(gameTime);
                    EquipInterface.Update(gameTime);
                    ToolTipInterface.Update(gameTime);
                }

                TimerInterface.Update(gameTime);
                MessageInterface.Update(gameTime);

                if (IsDropped)
                {
                    IsDropped = false;
                    if (DragDropSourceItem != null)
                    {
                        DragDropSourceItem.IsShow = true;
                    }
                    if (DragDropSourceTexture != null &&
                        DragDropSourceTexture.Data != null)
                    {
                        _dropSound.Play();
                    }
                    DragDropSourceItem = null;
                    DragDropSourceTexture = null;
                }
            }
            _lastKeyboardState = keyboardState;
            _lastMouseState = mouseState;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (SaveLoadInterface.IsShow)
            {
                SaveLoadInterface.Draw(spriteBatch);
            }
            else if (TitleInterface.IsShow)
            {
                TitleInterface.Draw(spriteBatch);
            }
            else
            {
                TimerInterface.Draw(spriteBatch);
                TopInterface.Draw(spriteBatch);
                BottomInterface.Draw(spriteBatch);
                ColumnInterface.Draw(spriteBatch);
                MagicInterface.Draw(spriteBatch);
                XiuLianInterface.Draw(spriteBatch);
                GoodsInterface.Draw(spriteBatch);
                BuyInterface.Draw(spriteBatch);
                MemoInterface.Draw(spriteBatch);
                StateInterface.Draw(spriteBatch);
                EquipInterface.Draw(spriteBatch);
                ToolTipInterface.Draw(spriteBatch);

                MessageInterface.Draw(spriteBatch);
                DialogInterface.Draw(spriteBatch);
            }

            MouseInterface.Draw(spriteBatch);
        }
    }
}