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
using WSR_2021.Model;
using WSR_2021.Utils;

namespace WSR_2021.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для KanbanBoard.xaml
    /// </summary>
    public partial class KanbanBoard : Page
    {
        #region Закрытые поля

        private Button grabbedBtn;
        private Button[] activityButtons;
        private Activity[] kanbanAct;

        #endregion

        #region Конструктор страницы KanbanBoard

        public KanbanBoard()
        {
            InitializeComponent();

            var allEvent = Transition.Context.Event.ToList();
            allEvent.Insert(0, new Event { Title = "Все мероприятия" });
            EventCBox.ItemsSource = allEvent;
            EventCBox.SelectedIndex = 0;
        }

        #endregion

        #region Создание отображаемых активностей выбранного мероприятия

        private void EventCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActivityCanvas.Children.Clear();

            if (EventCBox.SelectedIndex > 0)
            {
                int id = (EventCBox.SelectedItem as Event).Id;
                var evAct = Transition.Context.EventActivity.Where(p => p.EventId == id).ToList();

                kanbanAct = new Activity[evAct.Count];
                activityButtons = new Button[evAct.Count];

                for (int i = 0; i < kanbanAct.Length; i++)
                {
                    kanbanAct[i] = evAct[i].Activity;
                }

                int index = 0;
                int x = 100;
                int y = 0;
                for (int j = 0; j < activityButtons.Length; j++)
                {

                    if (index == activityButtons.Length)
                        break;

                    if (j == 4)
                    {
                        x = 400;
                        y = 0;
                    }
                    if (j == 8)
                    {
                        x = 700;
                        y = 0;
                    }

                    activityButtons[index] = new Button()
                    {
                        Width = 250,
                        Height = 70,
                        Content = new TextBlock()
                        {
                            Text = $"{kanbanAct[index].Title}, {kanbanAct[index].TimeActivity}, {kanbanAct[index].Users.Surname}",
                            TextWrapping = TextWrapping.Wrap,
                            TextAlignment = TextAlignment.Center
                        },
                        IsHitTestVisible = true
                    };
                    activityButtons[index].MouseMove += ButtonMove;//Исправить на левое нажатии мыши

                    ActivityCanvas.Children.Add(activityButtons[index]);
                    Canvas.SetLeft(ActivityCanvas.Children[index], x);
                    Canvas.SetTop(ActivityCanvas.Children[index], y);

                    index++;
                    y += 90;
                }
            }
        }

        #endregion

        #region Перемещение активностей по ActivityCanvas при зажатой ПКМ

        private void ButtonMove(object sender, MouseEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                grabbedBtn = (Button)sender;
                grabbedBtn.IsHitTestVisible = false;
                DragDrop.DoDragDrop(grabbedBtn, grabbedBtn, DragDropEffects.Move);
                grabbedBtn.IsHitTestVisible = true;
            }
        }

        private void ActivityCanvas_Drop(object sender, DragEventArgs e)
        {
            Point dropPosition = e.GetPosition(ActivityCanvas);
            Canvas.SetLeft(grabbedBtn, dropPosition.X);
            Canvas.SetTop(grabbedBtn, dropPosition.Y);
        }

        private void ActivityCanvas_DragOver(object sender, DragEventArgs e)
        {
            Point movePosition = e.GetPosition(ActivityCanvas);
            Canvas.SetLeft(grabbedBtn, movePosition.X);
            Canvas.SetTop(grabbedBtn, movePosition.Y);
        }

        #endregion

        //Установить библиотеку, имеющая функции для работы с PDF-файлами, и создать его для вывода активностей
        #region Создание PDF-файла 
        private void CreatePDFBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
