using Avalonia.Media;
using ReportCard.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace ReportCard.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Student[] students;

        private SolidColorBrush checkColor(float num)
        {
            if (num < 1) return new SolidColorBrush(Colors.Red);
            if (num < 1.5) return new SolidColorBrush(Colors.Yellow);
            else return new SolidColorBrush(Colors.Green);
        }

        private void CheckAverage(Student[] students)
        {
            for (int i = 0; i < 6; i++)
            {
                sc_scores[i] = 0;
            }
            for (int i = 0; i < students.Length; i++)
            {
                ScoreProgSr += students[i].Prog;
                ScoreITSr += students[i].IT;
                ScoreFisicsSr += students[i].Fisics;
                ScoreTRPOSr += students[i].TRPO;
                ScoreMathSr += students[i].Math;
                ScoreAverageSr += students[i].Average_Score;
            }
            ScoreProgSr /= students.Length;
            ColorProgSr = checkColor(ScoreProgSr);
            ScoreITSr /= students.Length;
            ColorITSr = checkColor(ScoreITSr);
            ScoreFisicsSr /= students.Length;
            ColorFisicsSr = checkColor(ScoreFisicsSr);
            ScoreTRPOSr /= students.Length;
            ColorTRPOSr = checkColor(ScoreTRPOSr);
            ScoreMathSr /= students.Length;
            ColorMathSr = checkColor(ScoreMathSr);
            ScoreAverageSr /= students.Length;
            ColorAverageSr = checkColor(ScoreAverageSr);
        }

        public MainWindowViewModel()
        {
            AddStudent = ReactiveCommand.Create(() =>
            {
                if (newName != "")
                {
                    Student[] temp = students;
                    Array.Resize(ref temp, temp.Length + 1);
                    temp[temp.Length - 1] = new Student { Name = newName, Prog = scores[0], IT = scores[1], Fisics = scores[2], TRPO = scores[3], Math = scores[4]};
                    Students = temp;
                    NewName = "";
                    ScoreProg = 0;
                    ScoreIT = 0;
                    ScoreFisics = 0;
                    ScoreTRPO = 0;
                    ScoreMath = 0;
                    CheckAverage(students);
                }
            });
            DeleteStudent = ReactiveCommand.Create(() =>
            {
                if (index < students.Length)
                {
                    Student[] temp = students;
                    for (int i = index; i < temp.Length - 1; i++)
                    {
                        temp[i] = temp[i + 1];
                    }
                    Array.Resize(ref temp, temp.Length - 1);
                    Students = temp;
                    Index = 5000;
                    CheckAverage(students);
                }
            });
            Save = ReactiveCommand.Create(() =>
            {
                Serializer<Student[]>.Save("data.dat", students);
            });
            Load = ReactiveCommand.Create(() =>
            {
                Students = Serializer<Student[]>.Load("data.dat");
                CheckAverage(students);
            });
            Students = new Student[]
            {
                new Student{Name="Леньшина Дарья Сергеевна", Prog=2, IT=2, Fisics=2, TRPO=2, Math=1},
                new Student{Name="Баньковский Вадим Игоревич", Prog=2, IT=2, Fisics=1, TRPO=1, Math=2},
            };
            CheckAverage(students);

        }

        public Student[] Students
        {
            get => students;
            set => this.RaiseAndSetIfChanged(ref students, value);
        }

        public ReactiveCommand<Unit, Unit> AddStudent { get; }
        public ReactiveCommand<Unit, Unit> DeleteStudent { get; }
        public ReactiveCommand<Unit, Unit> Save { get; }
        public ReactiveCommand<Unit, Unit> Load { get; }

        private ushort[] scores = { 0, 0, 0, 0, 0};
        private string newName = "";
        private ushort index = 5000;
        private float[] sc_scores = { 0, 0, 0, 0, 0, 0};
        private SolidColorBrush[] colorBrush = new SolidColorBrush[6];
        public ushort Index { get => index; set => this.RaiseAndSetIfChanged(ref index, value); }
        public string NewName { get => newName; set => this.RaiseAndSetIfChanged(ref newName, value); }
        public ushort ScoreProg { get => scores[0]; set => this.RaiseAndSetIfChanged(ref scores[0], value); }
        public ushort ScoreIT { get => scores[1]; set => this.RaiseAndSetIfChanged(ref scores[1], value); }
        public ushort ScoreFisics { get => scores[2]; set => this.RaiseAndSetIfChanged(ref scores[2], value); }
        public ushort ScoreTRPO { get => scores[3]; set => this.RaiseAndSetIfChanged(ref scores[3], value); }
        public ushort ScoreMath { get => scores[4]; set => this.RaiseAndSetIfChanged(ref scores[4], value); }

        public float ScoreProgSr { get => sc_scores[0]; set => this.RaiseAndSetIfChanged(ref sc_scores[0], value); }
        public float ScoreITSr { get => sc_scores[1]; set => this.RaiseAndSetIfChanged(ref sc_scores[1], value); }
        public float ScoreFisicsSr { get => sc_scores[2]; set => this.RaiseAndSetIfChanged(ref sc_scores[2], value); }
        public float ScoreTRPOSr { get => sc_scores[3]; set => this.RaiseAndSetIfChanged(ref sc_scores[3], value); }
        public float ScoreMathSr { get => sc_scores[4]; set => this.RaiseAndSetIfChanged(ref sc_scores[4], value); }
        public float ScoreAverageSr { get => sc_scores[5]; set => this.RaiseAndSetIfChanged(ref sc_scores[5], value); }

        public SolidColorBrush ColorProgSr { get => colorBrush[0]; set => this.RaiseAndSetIfChanged(ref colorBrush[0], value); }
        public SolidColorBrush ColorITSr { get => colorBrush[1]; set => this.RaiseAndSetIfChanged(ref colorBrush[1], value); }
        public SolidColorBrush ColorFisicsSr { get => colorBrush[2]; set => this.RaiseAndSetIfChanged(ref colorBrush[2], value); }
        public SolidColorBrush ColorTRPOSr { get => colorBrush[3]; set => this.RaiseAndSetIfChanged(ref colorBrush[3], value); }
        public SolidColorBrush ColorMathSr { get => colorBrush[4]; set => this.RaiseAndSetIfChanged(ref colorBrush[4], value); }
        public SolidColorBrush ColorAverageSr { get => colorBrush[5]; set => this.RaiseAndSetIfChanged(ref colorBrush[5], value); }
    }
}
