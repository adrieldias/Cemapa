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
        g.Clear(FromHex("#2c3e50"));
        for (int i = 0; i <= TabCount - 1; i++)
        {
            Rectangle tabRect = GetTabRect(i);
            if (i == SelectedIndex)
            {                
                if (i == 0)
                {
                    g.FillRectangle(new SolidBrush(FromHex("#34495e")), 0, 0, tabRect.Width + 2, tabRect.Height + 2);
                    g.FillRectangle(Brushes.DodgerBlue, 0, 0, 4, tabRect.Height + 2);                    
                }
                else
                {
                    g.FillRectangle(new SolidBrush(FromHex("#34495e")), tabRect);
                    g.FillRectangle(Brushes.DodgerBlue, tabRect.X - 2, tabRect.Y, 4, tabRect.Height);
                }
            }
            else if (!(_mouseOverTabIndex == -1) & i == _mouseOverTabIndex)
            {
                if (i == 0)
                {
                    g.FillRectangle(new SolidBrush(FromHex("#435363")), 0, 0, tabRect.Width + 3, tabRect.Height + 2);
                }
                else
                {
                    g.FillRectangle(new SolidBrush(FromHex("#435363")), 0, tabRect.Y, tabRect.Width + 3, tabRect.Height);
                }
            }
            else
            {
                g.FillRectangle(new SolidBrush(FromHex("#2c3e50")), tabRect);
            }

            
                string drawString = this.TabPages[i].Text;
                System.Drawing.Font drawFont = new System.Drawing.Font("Calibri Light", 10);
                System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.WhiteSmoke);
                float x = tabRect.Width / 2 - 55;
                float y = tabRect.Y + tabRect.Height / 2 - 8;          
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
        base.OnMouseMove(e);
        for (int i = 0; i <= TabPages.Count - 1; i++)
        {
            if (GetTabRect(i).Contains(e.Location))
            {
                _mouseOverTabIndex = i;
                break; // TODO: might not be correct. Was : Exit For
            }
            else
            {
                _mouseOverTabIndex = -1;
            }
        }
        Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        _mouseOverTabIndex = -1;
        Invalidate();
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