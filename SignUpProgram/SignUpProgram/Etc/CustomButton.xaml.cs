﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SignUpProgram
{
    /// <summary>
    /// Interaction logic for CustomButton.xaml
    /// </summary>
    public partial class CustomButton : Button
    {
        public CustomButton() : base()
        {
            Style style = new Style();
            style.TargetType = typeof(CustomButton);
            Setter setter = new Setter(CustomButton.HeightProperty, 300.0);
            Setter setter2 = new Setter(CustomButton.WidthProperty, 300.0);

            style.Setters.Add(setter);
            style.Setters.Add(setter2);

            this.Style = style;
        }

        //public event RoutedEventHandler Click
        //{
        //    add { AddHandler(ClickEvent, value); }
        //    remove { RemoveHandler(ClickEvent, value); }
        //}

        //protected virtual void OnClick()
        //{
        //    RoutedEventArgs args = new RoutedEventArgs(ClickEvent, this);
        //    RaiseEvent(args);
        //}

        //protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        //{
        //    base.OnMouseLeftButtonUp(e);
        //    OnClick();
        //}
    }
}
