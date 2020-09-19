﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Liftmanagement.Views
{
    /// <summary>
    /// An attached behavior that modifies the tab behavior for a <see cref="DataGrid"/>.
    /// </summary>
    public static class DataGridBehavior
    {
        /// <summary>
        /// Identifies the <c>NewLineOnTab</c> attached property.
        /// </summary>
        public static readonly DependencyProperty NewLineOnTabProperty = DependencyProperty.RegisterAttached(
            "NewLineOnTab",
            typeof(bool),
            typeof(DataGridBehavior),
            new PropertyMetadata(default(bool), OnNewLineOnTabChanged));

        /// <summary>
        /// Sets the value of the <c>NewLineOnTab</c> attached property.
        /// </summary>
        /// <param name="element">The <see cref="DataGrid"/>.</param>
        /// <param name="value">A value indicating whether to apply the behavior.</param>
        public static void SetNewLineOnTab(DataGrid element, bool value)
        {
            element.SetValue(NewLineOnTabProperty, value);
        }

        /// <summary>
        /// Gets the value of the <c>NewLineOnTab</c> attached property.
        /// </summary>
        /// <param name="element">The <see cref="DataGrid"/>.</param>
        /// <returns>A value indicating whether to apply the behavior.</returns>
        public static bool GetNewLineOnTab(DataGrid element)
        {
            return (bool)element.GetValue(NewLineOnTabProperty);
        }

        /// <summary>
        /// Called when the value of the <c>NewLineOnTab</c> property changes.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private static void OnNewLineOnTabChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DataGrid d = sender as DataGrid;

            if (d == null)
            {
                return;
            }

            bool newValue = (bool)e.NewValue;
            bool oldValue = (bool)e.OldValue;

            if (oldValue == newValue)
            {
                return;
            }

            if (oldValue)
            {
                d.PreviewKeyDown -= AssociatedObjectKeyDown;
            }
            else
            {
                d.PreviewKeyDown += AssociatedObjectKeyDown;
                KeyboardNavigation.SetTabNavigation(d, KeyboardNavigationMode.Contained);
            }
        }

        /// <summary>
        /// Handles the <see cref="UIElement.KeyDown"/> event for a <see cref="DataGridCell"/>.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private static void AssociatedObjectKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Tab)
            {
                return;
            }

            DataGrid dg = e.Source as DataGrid;

            if (dg == null)
            {
                return;
            }

            if (dg.CurrentColumn.DisplayIndex == dg.Columns.Count - 1)
            {
                var icg = dg.ItemContainerGenerator;

                if (dg.SelectedIndex == icg.Items.Count - 2)
                {
                    dg.CommitEdit(DataGridEditingUnit.Row, false);
                }
            }
        }
    }
}
//https://stackoverflow.com/questions/13601295/tab-create-new-row-and-focus-new-row-in-datagrid