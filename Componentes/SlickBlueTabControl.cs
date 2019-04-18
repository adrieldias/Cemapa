using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

public class SlickBlueTabControl : TabControl
{
    private int _mouseOverTabIndex = 0;
    public SlickBlueTabControl()
    {
        //Dock = DockStyle.Fill;
        Dock = DockStyle.None;
        Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
        DoubleBuffered = true;
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        SizeMode = TabSizeMode.Fixed;
        ItemSize = new Size(40, 130);
        Alignment = TabAlignment.Left;
        Font = new Font("Calibri Light", 10);
    }

    protected override void OnPaint(PaintEventArgs e)
    {        
        Bitmap b = new Bitmap(Width, Height);
        Graphics g = Graphics.FromImage(b);
        //g.Clear(FromHex("#2c3e50"));
        g.Clear(FromHex("#EEE9E9"));
        for (int i = 0; i <= TabCount - 1; i++)
        {
            Rectangle tabRect = GetTabRect(i);            
            if (i == SelectedIndex)
            {                
                if (i == 0)
                {
                    g.FillRectangle(new SolidBrush(/*FromHex("#34495e")*/FromHex("#EEE9E9")), 0, 0, tabRect.Width + 2, tabRect.Height + 2);
                    g.FillRectangle(Brushes.SteelBlue, 0, 0, 4, tabRect.Height + 2);                    
                }
                else
                {
                    g.FillRectangle(new SolidBrush(/*FromHex("#34495e")*/FromHex("#EEE9E9")), tabRect);
                    g.FillRectangle(Brushes.SteelBlue, tabRect.X - 2, tabRect.Y, 4, tabRect.Height);
                }
            }
            else if (!(_mouseOverTabIndex == -1) & i == _mouseOverTabIndex)
            {
                //if (i == 0)
                //{
                //    g.FillRectangle(new SolidBrush(/*FromHex("#435363")*/Color.LightGray), 0, 0, tabRect.Width + 3, tabRect.Height + 2);
                //}
                //else
                //{
                //    g.FillRectangle(new SolidBrush(/*FromHex("#435363")*/Color.LightGray), 0, tabRect.Y, tabRect.Width + 3, tabRect.Height);
                //}

                //ToolTipText
                //string drawToolTip = this.TabPages[i].ToolTipText;
                //System.Drawing.Font drawToolTipFont = new System.Drawing.Font("Calibri Light", 6);
                //System.Drawing.SolidBrush drawToolTipBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Gray);
                //float xToolTip = tabRect.Left;
                //float yToolTip = tabRect.Bottom - 12;
                //System.Drawing.StringFormat drawToolTipFormat = new System.Drawing.StringFormat();
                //g.DrawString(drawToolTip.ToUpper(), drawToolTipFont, drawToolTipBrush, xToolTip, yToolTip, drawToolTipFormat);
                //drawToolTipFont.Dispose();
                //drawToolTipBrush.Dispose();                
            }
            else
            {
                g.FillRectangle(new SolidBrush(/*FromHex("#2c3e50")*/FromHex("#EEE9E9")), tabRect);
            }

                //Labels das abas
                string drawString = this.TabPages[i].Text;
                System.Drawing.Font drawFont = new System.Drawing.Font("Calibri Light", 9);
                System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.SteelBlue);
                float x = tabRect.Width / 2 - 55;
                float y = tabRect.Y + tabRect.Height / 2 - 6;          
                System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                g.DrawString(drawString.ToUpper(), drawFont, drawBrush, x, y, drawFormat);
                drawFont.Dispose();
                drawBrush.Dispose();
        }
        e.Graphics.DrawImage(b, 0, 0);
        g.Dispose();
        b.Dispose();

        base.OnPaint(e);
    }    

    protected override void OnMouseMove(MouseEventArgs e)
    {
        //base.OnMouseMove(e);
        //for (int i = 0; i <= TabPages.Count - 1; i++)
        //{
        //    if (GetTabRect(i).Contains(e.Location))
        //    {
        //        _mouseOverTabIndex = i;
        //        break; // TODO: might not be correct. Was : Exit For
        //    }
        //    else
        //    {
        //        _mouseOverTabIndex = -1;
        //    }
        //}
        //Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        //base.OnMouseLeave(e);
        //_mouseOverTabIndex = -1;
        //Invalidate();
    }

    #region "Helpers"
    public enum MouseState
    {
        Hover = 1,
        Down = 2,
        None = 3
    }

    public Color FromHex(string hex)
    {
        return ColorTranslator.FromHtml(hex.Replace("#", "").Insert(0, "#"));
    }
    #endregion
}