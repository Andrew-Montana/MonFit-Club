using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MonFit_Club.Infrastructure
{
    /// <span class="code-SummaryComment"><summary>
    /// This class adds binding capabilities to the standard WPF PasswordBox.
    /// <span class="code-SummaryComment"></summary>
    public class BoundPasswordBox
    {
        #region BoundPassword
        private static bool _updating = false;

        /// <span class="code-SummaryComment"><summary>
        /// BoundPassword Attached Dependency Property
        /// <span class="code-SummaryComment"></summary>
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached("BoundPassword",
                typeof(string),
                typeof(BoundPasswordBox),
                new FrameworkPropertyMetadata(string.Empty, OnBoundPasswordChanged));

        /// <span class="code-SummaryComment"><summary>
        /// Gets the BoundPassword property.
        /// <span class="code-SummaryComment"></summary>
        public static string GetBoundPassword(DependencyObject d)
        {
            return (string)d.GetValue(BoundPasswordProperty);
        }

        /// <span class="code-SummaryComment"><summary>
        /// Sets the BoundPassword property.
        /// <span class="code-SummaryComment"></summary>
        public static void SetBoundPassword(DependencyObject d, string value)
        {
            d.SetValue(BoundPasswordProperty, value);
        }

        /// <span class="code-SummaryComment"><summary>
        /// Handles changes to the BoundPassword property.
        /// <span class="code-SummaryComment"></summary>
        private static void OnBoundPasswordChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox password = d as PasswordBox;
            if (password != null)
            {
                // Disconnect the handler while we're updating.
                password.PasswordChanged -= PasswordChanged;
            }

            if (e.NewValue != null)
            {
                if (!_updating)
                {
                    password.Password = e.NewValue.ToString();
                }
            }
            else
            {
                password.Password = string.Empty;
            }
            // Now, reconnect the handler.
            password.PasswordChanged += new RoutedEventHandler(PasswordChanged);
        }

        /// <span class="code-SummaryComment"><summary>
        /// Handles the password change event.
        /// <span class="code-SummaryComment"></summary>
        static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox password = sender as PasswordBox;
            _updating = true;
            SetBoundPassword(password, password.Password);
            _updating = false;
        }

        #endregion
    }
}
