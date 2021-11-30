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

        Button grabbedBtn;
        private Button[] activityButtons;
        private StackPanel[] activityPanel;
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

                int totalStackPanel = (int)Math.Ceiling((decimal)evAct.Count / 3) + 1;
                activityPanel = new StackPanel[totalStackPanel];
                kanbanAct = new Activity[evAct.Count];
                activityButtons = new Button[evAct.Count];

                for (int i = 0; i < kanbanAct.Length; i++)
                {
                    int tempId = evAct[i].ActivityId;
                    kanbanAct[i] = Transition.Context.Activity.FirstOrDefault(p => p.Id == tempId);
                }

                int index = 0;
                for (int i = 0; i < activityPanel.Length; i++)
                {
                    HorizontalAlignment horizontal = HorizontalAlignment.Left;
                    switch (i)
                    {
                        case 0:
                            horizontal = HorizontalAlignment.Left;
                            break;
                        case 1:
                            horizontal = HorizontalAlignment.Center;
                            break;
                        case 2:
                            horizontal = HorizontalAlignment.Right;
                            break;
                    }
                    activityPanel[i] = new StackPanel() { Margin = new Thickness(20), HorizontalAlignment = horizontal};

                    for (int j = 0; j < (int)Math.Ceiling((decimal)activityButtons.Length / 3); j++)
                    {
                        if (index == activityButtons.Length)
                            break;

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
                        
                        index++;
                    }
                }
            }
        }

        #endregion

        private void ButtonMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
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
    }
}
