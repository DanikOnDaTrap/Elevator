using System;
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

namespace Elevator
{
    public partial class MainWindow : Window
    {
        Lift lift = new Lift();
        RadioButton lastRb;

        public MainWindow()
        {
            InitializeComponent();
            tb_diplayLiftMovement.Text += $"{lift.CurrFloor}-ый этаж: {statusToStr(lift.DoorsStatus)}\n";
        }

        private void tb_countFloors_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_countFloors.Text != "" && int.TryParse(tb_countFloors.Text, out int intVal)
                && lastRb != null && rb_doorsClosed.IsChecked.Value)
            {
                btn_startMove.IsEnabled = true;
            }
            else
            {
                btn_startMove.IsEnabled = false;
            }

            if (tb_countFloors.Text != "")
                lift.CountFloor = int.Parse(tb_countFloors.Text);
            lb_currFloor.Content = lift.CurrFloor.ToString();


        }

        private void tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void rb_moveChecked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb)
            {
                lastRb = rb;
            }

            if (lastRb != null && tb_countFloors.Text != "" && rb_doorsClosed.IsChecked.Value)
            {
                btn_startMove.IsEnabled = true;
            }
            else
            {
                btn_startMove.IsEnabled = false;
            }
        }

        private void btn_startMove_Click(object sender, RoutedEventArgs e)
        {
            switch (lastRb.Content)
            {
                case "Вверх":
                    {
                        lift.MoveUp();
                    }
                    break;
                case "Вниз":
                    {
                        lift.MoveDown();
                    }
                    break;
                default:
                    break;
            }
            tb_diplayLiftMovement.Text += $"{lift.CurrFloor}-ый этаж: {statusToStr(lift.DoorsStatus)}\n";

            if (lift.CurrFloor == lift.CountFloor || lift.CurrFloor == 1)
            {
                MessageBox.Show("Невозможно продолжить движение!",
                                 "Внимание",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Warning);
            }
            lb_currFloor.Content = lift.CurrFloor.ToString();
        }
        private void rb_doorsChecked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb)
            {
                switch (rb.Content)
                {
                    case "Открыть двери":
                        {
                            lift.DoorsStatus = Status.Opened;
                            btn_startMove.IsEnabled = false;
                        }
                        break;
                    case "Закрыть двери":
                        {
                            lift.DoorsStatus = Status.Closed;

                            if (tb_countFloors.Text != "" && lastRb.IsChecked.Value)
                            {
                                btn_startMove.IsEnabled = true;
                            }
                        }
                        break;
                    default:
                        break;
                }
                tb_diplayLiftMovement.Text += $"{lift.CurrFloor}-ый этаж: {statusToStr(lift.DoorsStatus)}\n";
            }
        }
        private string statusToStr(Status statusDoors)
        {
            switch (statusDoors)
            {
                case Status.Closed: return "Двери закрыты";
                case Status.Opened: return "Двери открыты";
                default: return "Двери не работают";
            }
        }
    }
}
