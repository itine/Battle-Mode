using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Data.SqlClient;

namespace ConfigStat
{
    public partial class Form1 : Form
    {
        GameEntities db = new GameEntities();
        List<int> leftMonsters = new List<int>();
        List<int> rightMonsters = new List<int>();
        List<units> localLeftMonsters = new List<units>();
        List<units> localRightMonsters = new List<units>();
        
        public static int step = 1;
        public static double damage = 0;
        int parityCount = 0;
        int currentRightTextBox = -1;
        int currentLeftTextBox = -1;
        int countOfRightMonsters = 1;
        int countOfLeftMonsters = 1;

        int leftMonsterAttack = 0;
        int leftMonsterHP = 0;
        int leftMonsterAverageDamage = 0;
        int leftMonsterDefence = 0;
        int leftMonsterMinDamage = 0;
        int leftMonsterMaxDamage = 0;
        int currentHPOfLeftMonster = 0;
        double monstersRemaningOnLeft = 0;
        double remainingHPOnTheLeft = 0;
        
        int currentHPOfRightMonster = 0;
        double monstersRemaningOnRight = 0;
        double remainingHPOnTheRight = 0;
        int rightMonsterHP = 0;
        int rightMonsterMinDamage = 0;
        int rightMonsterMaxDamage = 0;
        int rightMonsterAverageDamage = 0;
        int rightMonsterAttack = 0;
        int rightMonsterDefence = 0;

        double coefficientForLeft = 1.0;
        double coefficientForRight = 1.0;
        int luckForLeft = 0;
        int luckForRight = 0;
        int moraleForLeft = 0;
        int moraleForRight = 0;
        bool flagForLeft = false;
        bool flagForRight = false;
        List<int> localLeftM = new List<int>();
        List<int> localRightM = new List<int>();

        Label[] leftLabels = new Label[7];
        Label[] rightLabels = new Label[7];

        PictureBox[] leftBoxs = new PictureBox[7];
        PictureBox[] rightBoxs = new PictureBox[7];

        TextBox[] leftTextBoxs = new TextBox[7];
        TextBox[] rightTextBoxs = new TextBox[7];

        RadioButton[] leftRadioButtons = new RadioButton[5];
        RadioButton[] rightRadioButtons = new RadioButton[5];

        public Form1()
        {
            InitializeComponent();

            leftLabels[0] = label29;
            leftLabels[1] = label30;
            leftLabels[2] = label31;
            leftLabels[3] = label32;
            leftLabels[4] = label33;
            leftLabels[5] = label34;
            leftLabels[6] = label35;

            rightLabels[0] = label42;
            rightLabels[1] = label41;
            rightLabels[2] = label40;
            rightLabels[3] = label39;
            rightLabels[4] = label38;
            rightLabels[5] = label37;
            rightLabels[6] = label36;

            leftRadioButtons[0] = radioButton3;
            leftRadioButtons[1] = radioButton4;
            leftRadioButtons[2] = radioButton5;
            leftRadioButtons[3] = radioButton6;
            leftRadioButtons[4] = radioButton7;

            rightRadioButtons[0] = radioButton12;
            rightRadioButtons[1] = radioButton11;
            rightRadioButtons[2] = radioButton10;
            rightRadioButtons[3] = radioButton9;
            rightRadioButtons[4] = radioButton8;

            leftTextBoxs[0] = textBox14;
            leftTextBoxs[1] = textBox13;
            leftTextBoxs[2] = textBox12;
            leftTextBoxs[3] = textBox11;
            leftTextBoxs[4] = textBox10;
            leftTextBoxs[5] = textBox9;
            leftTextBoxs[6] = textBox8;

            rightTextBoxs[0] = textBox1;
            rightTextBoxs[1] = textBox2;
            rightTextBoxs[2] = textBox3;
            rightTextBoxs[3] = textBox4;
            rightTextBoxs[4] = textBox5;
            rightTextBoxs[5] = textBox6;
            rightTextBoxs[6] = textBox7;

            leftBoxs[0] = pictureBox14;
            leftBoxs[1] = pictureBox13;
            leftBoxs[2] = pictureBox12;
            leftBoxs[3] = pictureBox11;
            leftBoxs[4] = pictureBox10;
            leftBoxs[5] = pictureBox9;
            leftBoxs[6] = pictureBox8;

            rightBoxs[0] = pictureBox1;
            rightBoxs[1] = pictureBox2;
            rightBoxs[2] = pictureBox3;
            rightBoxs[3] = pictureBox4;
            rightBoxs[4] = pictureBox5;
            rightBoxs[5] = pictureBox6;
            rightBoxs[6] = pictureBox7;
        }

        private void CheckPlayerClasses()
        {
            int leftClass = -1;
            int rightClass = -1;
            for (int i = 0; i <= leftRadioButtons.Length - 1; i++)
            {
                if (leftRadioButtons[i].Checked)
                {
                    leftClass = i;
                    break;
                }
            }
            for (int i = 0; i <= rightRadioButtons.Length - 1; i++)
            {
                if (rightRadioButtons[i].Checked)
                {
                    rightClass = i;
                    break;
                }
            }
            if ((leftClass == 0) && (rightClass == 1))
                coefficientForLeft += 0.06;
            if ((leftClass == 0) && (rightClass == 3))
                coefficientForLeft += 0.06;
            if ((leftClass == 1) && (rightClass == 3))
                coefficientForLeft += 0.06;
            if ((leftClass == 1) && (rightClass == 4))
                coefficientForLeft += 0.06;
            if ((leftClass == 2) && (rightClass == 0))
                coefficientForLeft += 0.06;
            if ((leftClass == 2) && (rightClass == 1))
                coefficientForLeft += 0.06;
            if ((leftClass == 3) && (rightClass == 4))
                coefficientForLeft += 0.06;
            if ((leftClass == 3) && (rightClass == 2))
                coefficientForLeft += 0.06;
            if ((leftClass == 4) && (rightClass == 2))
                coefficientForLeft += 0.06;
            if ((leftClass == 4) && (rightClass == 0))
                coefficientForLeft += 0.06;

            if ((rightClass == 0) && (leftClass == 1))
                coefficientForRight += 0.06;
            if ((rightClass == 0) && (leftClass == 3))
                coefficientForRight += 0.06;
            if ((rightClass == 1) && (leftClass == 3))
                coefficientForRight += 0.06;
            if ((rightClass == 1) && (leftClass == 4))
                coefficientForRight += 0.06;
            if ((rightClass == 2) && (leftClass == 0))
                coefficientForRight += 0.06;
            if ((rightClass == 2) && (leftClass == 1))
                coefficientForRight += 0.06;
            if ((rightClass == 3) && (leftClass == 4))
                coefficientForRight += 0.06;
            if ((rightClass == 3) && (leftClass == 2))
                coefficientForRight += 0.06;
            if ((rightClass == 4) && (leftClass == 2))
                coefficientForRight += 0.06;
            if ((rightClass == 4) && (leftClass == 0))
                coefficientForRight += 0.06;

           //.. leftRadioButtons[leftClass] 

        }

        private double GetCoefficientOfAttack(string attackerClassName, int attackerLevel, string defenderClassName, int defenderLevel)
        {
            double totalCoefficient = 1.0;
            if (attackerClassName.Equals("attacker") && defenderClassName.Equals("shooter"))
                totalCoefficient += 0.2;
            if (attackerClassName.Equals("shooter") && defenderClassName.Equals("flyer"))
                totalCoefficient += 0.2;
            if (attackerClassName.Equals("flyer") && defenderClassName.Equals("attacker"))
                totalCoefficient += 0.2;
            if ((attackerLevel == 1) && (defenderLevel == 2))
                totalCoefficient += 0.1;
            if ((attackerLevel == 1) && (defenderLevel == 3))
                totalCoefficient += 0.2;
            if ((attackerLevel == 2) && (defenderLevel == 3))
                totalCoefficient += 0.1;
            return totalCoefficient;
        }
        
        private void UpdateQuantityOnLeft(int idUnit, double quantity)
        {
            units unit = db.units.Find(idUnit);
            unit.monstersRemainingOnLeft = quantity;
            db.Entry(unit).State = EntityState.Modified;
            db.SaveChanges();
        }

        private void UpdateQuantityOnRight(int idUnit, double quantity)
        {
            units unit = db.units.Find(idUnit);
            unit.monstersRemainingOnRight = quantity;
            db.Entry(unit).State = EntityState.Modified;
            db.SaveChanges();
        }

        private void ModifiedQuantityForLeft(int idUnit, int currentTbId)
        {
            units unit = db.units.Find(idUnit);
            unit.currentLeftBox = currentTbId;
            if (step == 1)
                unit.monstersRemainingOnLeft = Double.Parse(leftTextBoxs[currentTbId-1].Text);
            
            db.Entry(unit).State = EntityState.Modified;
            db.SaveChanges();
        }

        private void ModifiedQuantityForRight(int idUnit, int currentTbId)
        {
            units unit = db.units.Find(idUnit);
            unit.currentRightBox = currentTbId;
            if (step == 1)
                unit.monstersRemainingOnRight = Double.Parse(rightTextBoxs[currentTbId-1].Text);
            
            db.Entry(unit).State = EntityState.Modified;
            db.SaveChanges();
        }

        private void CreateNewListOfMonstersAndUpdateQuantity(string side)
        {
            if (side == "left")
            {
                for (int i = 0; i <= leftMonsters.Count - 1; i++)
                {
                    int currentID = leftMonsters[i];
                    if (leftTextBoxs[i].BackColor == Color.WhiteSmoke)
                        continue;
                    else
                    {
                        var unit = (from a in db.units
                                    where a.idUnit == currentID
                                    select a).Single();
                        localLeftMonsters.Add(unit);
                        ModifiedQuantityForLeft(currentID, i + 1);
                    }

                }
                countOfLeftMonsters = localLeftMonsters.Count;
            }
            else
            {
                for (int i = 0; i <= rightMonsters.Count - 1; i++)
                {
                    int currentID = rightMonsters[i];
                    if (rightTextBoxs[i].BackColor == Color.WhiteSmoke)
                        continue;
                    else
                    {
                        var unit = (from a in db.units
                                    where a.idUnit == currentID
                                    select a).Single();
                        localRightMonsters.Add(unit);
                        ModifiedQuantityForRight(currentID, i + 1);
                    }
                }
                countOfRightMonsters = localRightMonsters.Count;
            }

        }

        private int GetRightTextBox(int currentTextBox)
        {
            int neededTextBox = currentTextBox;
            if (currentTextBox == 0)
            {
                for (int i = 1; i <= rightTextBoxs.Length - 1; i++)
                {
                    if (rightTextBoxs[i].BackColor == Color.WhiteSmoke)
                        continue;
                    return neededTextBox = i;
                }

            }
            else if (currentTextBox == 1)
            {
                if (rightTextBoxs[currentTextBox - 1].BackColor != Color.WhiteSmoke)
                {
                    return neededTextBox = currentTextBox - 1;
                }
                else
                {
                    for (int j = 2; j <= 6; j++)
                    {
                        if (rightTextBoxs[j].BackColor != Color.WhiteSmoke)
                        {
                            neededTextBox = j;
                            return neededTextBox;
                        }
                    }
                }
            }
            else if (currentTextBox == 2)
            {
                int i = 1;
                while (i != 3)
                {
                    if (rightTextBoxs[currentTextBox - i].BackColor != Color.WhiteSmoke)
                    {
                        return neededTextBox = currentTextBox - i;
                    }
                    else if (rightTextBoxs[currentTextBox + i].BackColor != Color.WhiteSmoke)
                    {
                        return neededTextBox = currentTextBox + i;
                    }
                    i++;
                }
                for (int j = 5; j <= 6; j++)
                {
                    if (rightTextBoxs[j].BackColor != Color.WhiteSmoke)
                    {
                        neededTextBox = j;
                        return neededTextBox;
                    }
                }
            }
            else if (currentTextBox == 3)
            {
                int i = 1;
                while (i != 4)
                {
                    if (rightTextBoxs[currentTextBox - i].BackColor != Color.WhiteSmoke)
                    {
                        return neededTextBox = currentTextBox - i;
                    }
                    else if (rightTextBoxs[currentTextBox + i].BackColor != Color.WhiteSmoke)
                    {
                        return neededTextBox = currentTextBox + i;
                    }
                    i++;
                }
            }
            else if (currentTextBox == 4)
            {
                int i = 1;
                while (i != 3)
                {
                    if (rightTextBoxs[currentTextBox - i].BackColor != Color.WhiteSmoke)
                    {
                        return neededTextBox = currentTextBox - i;
                    }
                    else if (rightTextBoxs[currentTextBox + i].BackColor != Color.WhiteSmoke)
                    {
                        return neededTextBox = currentTextBox + i;
                    }
                    i++;
                }
                for (int j = 2; j >= 0; j--)
                {
                    if (rightTextBoxs[j].BackColor != Color.WhiteSmoke)
                    {
                        neededTextBox = j;
                        return neededTextBox;
                    }
                }
            }

            else if (currentTextBox == 5)
            {
                if (rightTextBoxs[currentTextBox - 1].BackColor != Color.WhiteSmoke)
                {
                    return neededTextBox = currentTextBox - 1;
                }
                else if (rightTextBoxs[currentTextBox + 1].BackColor != Color.WhiteSmoke)
                {
                    return neededTextBox = currentTextBox + 1;
                }
                else
                {
                    for (int j = 3; j >= 0; j--)
                    {
                        if (rightTextBoxs[j].BackColor != Color.WhiteSmoke)
                        {
                            neededTextBox = j;
                            return neededTextBox;
                        }
                    }
                }
            }

            else if (currentTextBox == 6)
            {
                for (int i = rightTextBoxs.Length - 2; i >= 0; i--)
                {
                    if (rightTextBoxs[i].BackColor == Color.WhiteSmoke)
                        continue;
                    return neededTextBox = i;
                }
            }
            return neededTextBox;
        }

        private int GetLeftTextBox(int currentTextBox)
        {
            int neededTextBox = currentTextBox;
            if (currentTextBox == 0)
            {
                for (int i = 1; i <= leftTextBoxs.Length - 1; i++)
                {
                    if (leftTextBoxs[i].BackColor == Color.WhiteSmoke)
                        continue;
                    return neededTextBox = i;
                }

            }
            else if (currentTextBox == 1)
            {
                if (leftTextBoxs[currentTextBox - 1].BackColor != Color.WhiteSmoke)
                {
                    return neededTextBox = currentTextBox - 1;
                }
                else
                {
                    for (int j = 2; j <= 6; j++)
                    {
                        if (leftTextBoxs[j].BackColor != Color.WhiteSmoke)
                        {
                            neededTextBox = j;
                            return neededTextBox;
                        }
                    }
                }
            }
            else if (currentTextBox == 2)
            {
                int i = 1;
                while (i != 3)
                {
                    if (leftTextBoxs[currentTextBox - i].BackColor != Color.WhiteSmoke)
                    {
                        return neededTextBox = currentTextBox - i;
                    }
                    else if (leftTextBoxs[currentTextBox + i].BackColor != Color.WhiteSmoke)
                    {
                        return neededTextBox = currentTextBox + i;
                    }
                    i++;
                }
                for (int j = 5; j <= 6; j++)
                {
                    if (leftTextBoxs[j].BackColor != Color.WhiteSmoke)
                    {
                        neededTextBox = j;
                        return neededTextBox;
                    }
                }
            }
            else if (currentTextBox == 3)
            {
                int i = 1;
                while (i != 4)
                {
                    if (leftTextBoxs[currentTextBox - i].BackColor != Color.WhiteSmoke)
                    {
                        return neededTextBox = currentTextBox - i;
                    }
                    else if (leftTextBoxs[currentTextBox + i].BackColor != Color.WhiteSmoke)
                    {
                        return neededTextBox = currentTextBox + i;
                    }
                    i++;
                }
            }
            else if (currentTextBox == 4)
            {
                int i = 1;
                while (i != 3)
                {
                    if (leftTextBoxs[currentTextBox - i].BackColor != Color.WhiteSmoke)
                    {
                        return neededTextBox = currentTextBox - i;
                    }
                    else if (leftTextBoxs[currentTextBox + i].BackColor != Color.WhiteSmoke)
                    {
                        return neededTextBox = currentTextBox + i;
                    }
                    i++;
                }
                for (int j = 2; j >= 0; j--)
                {
                    if (leftTextBoxs[j].BackColor != Color.WhiteSmoke)
                    {
                        neededTextBox = j;
                        return neededTextBox;
                    }
                }
            }

            else if (currentTextBox == 5)
            {
                if (leftTextBoxs[currentTextBox - 1].BackColor != Color.WhiteSmoke)
                {
                    return neededTextBox = currentTextBox - 1;
                }
                else if (leftTextBoxs[currentTextBox + 1].BackColor != Color.WhiteSmoke)
                {
                    return neededTextBox = currentTextBox + 1;
                }
                else
                {
                    for (int j = 3; j >= 0; j--)
                    {
                        if (leftTextBoxs[j].BackColor != Color.WhiteSmoke)
                        {
                            neededTextBox = j;
                            return neededTextBox;
                        }
                    }
                }
            }

            else if (currentTextBox == 6)
            {
                for (int i = leftTextBoxs.Length - 2; i >= 0; i--)
                {
                    if (leftTextBoxs[i].BackColor == Color.WhiteSmoke)
                        continue;
                    return neededTextBox = i;
                }
            }
            return neededTextBox;
        }

        private void DetermineSide(int idLeft, int idRight)
        {
            if (GetLeftUnitById(idLeft).speed >= GetRightUnitById(idRight).speed)
                parityCount = 1;
            else
                parityCount = 2;
        }

        private units GetLeftUnitById(int id)
        {
            units unit = (from a in db.units
                          where a.idUnit == id && a.currentLeftBox != null
                          select a).Single();
            return unit;
        }

        private units GetRightUnitById(int id)
        {
            units unit = (from a in db.units
                          where a.idUnit == id && a.currentRightBox != null
                          select a).Single();
            return unit;
        }

        private units GetUnitByName(string name)
        {
            units unit = (from a in db.units
                          where a.unitName == name
                          select a).Single();
            return unit;
        }
        private int GetIdOnTheLeft()
        {
            if (localLeftMonsters.Count == 0)
                return -1;
            int count = 0;
            foreach (var tb in leftTextBoxs)
            {
                if ((tb.BackColor == Color.Aquamarine) || (tb.BackColor == Color.WhiteSmoke))
                {
                    count++;
                }
            }
            int remainingMonstersWithEnergy = 0;
            if (count == leftTextBoxs.Length)
            {
                foreach (var rb in rightTextBoxs)
                {
                    //если остались неходившиеся
                    if ((rb.BackColor != Color.Aquamarine) && (rb.BackColor != Color.WhiteSmoke))
                        remainingMonstersWithEnergy++;
                }
                //если не остались, то       
                if (remainingMonstersWithEnergy == 0)
                {
                    foreach (var tb in leftTextBoxs)
                    {
                        if (tb.BackColor != Color.WhiteSmoke)
                            tb.BackColor = Color.White;
                    }
                    foreach (var tb in rightTextBoxs)
                    {
                        if (tb.BackColor != Color.WhiteSmoke)
                            tb.BackColor = Color.White;
                    }
                }
                else
                {
                    parityCount = 2;
                    return 1;
                }
            }
            for (int i = 0; i <= localLeftMonsters.Count - 1; i++)
            {
                int leftBoxId = localLeftMonsters[i].currentLeftBox.GetValueOrDefault() - 1;
                if ((leftTextBoxs[leftBoxId].BackColor != Color.Aquamarine) && (Double.Parse(leftTextBoxs[localLeftMonsters[i].currentLeftBox.GetValueOrDefault() - 1].Text) > 0))
                    return localLeftMonsters[i].idUnit;

            }
            return -1;
        }
        private int GetIdOnTheRight()
        {
            if (localRightMonsters.Count == 0)
                return -1;
            int count = 0;
            foreach (var tb in rightTextBoxs)
            {
                if ((tb.BackColor == Color.Aquamarine) || (tb.BackColor == Color.WhiteSmoke))
                {
                    count++;
                }
            }
            int remainingMonstersWithEnergy = 0;
            if (count == rightTextBoxs.Length)
            {
                foreach (var lb in leftTextBoxs)
                {
                    //если остались неходившиеся
                    if ((lb.BackColor != Color.Aquamarine) && (lb.BackColor != Color.WhiteSmoke))
                        remainingMonstersWithEnergy++;
                }
                //если не остались, то       
                if (remainingMonstersWithEnergy == 0)
                {
                    foreach (var tb in rightTextBoxs)
                    {
                        if (tb.BackColor != Color.WhiteSmoke)
                            tb.BackColor = Color.White;
                    }
                    foreach (var tb in leftTextBoxs)
                    {
                        if (tb.BackColor != Color.WhiteSmoke)
                            tb.BackColor = Color.White;
                    }
                }
                else
                {
                    parityCount = 1;
                    return 1;
                }

            }
            for (int i = 0; i <= localRightMonsters.Count - 1; i++)
            {
                int rightBoxId = localRightMonsters[i].currentRightBox.GetValueOrDefault() - 1;
                if ((rightTextBoxs[rightBoxId].BackColor != Color.Aquamarine) && (Double.Parse(rightTextBoxs[localRightMonsters[i].currentRightBox.GetValueOrDefault() - 1].Text) > 0))
                    return localRightMonsters[i].idUnit;

            }
            return -1;
        }
        
        private bool CalculateLuck(int luck)
        {
            Random rand = new Random();
            int local = rand.Next(1, 10);
            if (luck >= local)
                return true;
            return false;
        }


        private int CalculateChanceOfDoubleDamageForLeft(int Morale, int currentTextBox)
        {
            if (localLeftM.Count != 0)
            {
                for (int i = 0; i <= localLeftM.Count - 1; i++)
                {
                    if (localLeftM[i] == currentTextBox)
                        return 1;
                }
            }
            Random rand = new Random();
            int local = rand.Next(1, 10);
            if (Morale >= local)
            {
                localLeftM.Add(currentTextBox);
                return 2;
            }
            return 1;
        }

        private int CalculateChanceOfDoubleDamageForRight(int Morale, int currentTextBox)
        {
            if (localRightM.Count != 0)
            {
                for (int i = 0; i <= localRightM.Count - 1; i++)
                {
                    if (localRightM[i] == currentTextBox)
                        return 1;
                }
            }
            Random rand = new Random();
            int local = rand.Next(1, 10);
            if (Morale >= local)
            {
                localRightM.Add(currentTextBox);
                return 2;
            }
            return 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (step % 18 == 0)
            {
                label6.Text = " ";
                label8.Text = " ";
            }
            if (step == 1)
            {
                luckForLeft = Int32.Parse(textBox21.Text);
                luckForRight = Int32.Parse(textBox24.Text);
                moraleForLeft = (int)domainUpDown1.SelectedItem;
                moraleForRight = (int)domainUpDown2.SelectedItem;
                int countOfChecked = 0;
                foreach (var rb in leftRadioButtons)
                {
                    if (rb.Checked)
                        countOfChecked++;
                }
                if (countOfChecked == 0)
                {
                    MessageBox.Show("не выбран класс ататующего героя");
                    return;
                }
                foreach (var rb in rightRadioButtons)
                {
                    if (rb.Checked)
                        countOfChecked++;
                }
                if (countOfChecked == 1)
                {
                    MessageBox.Show("не выбран класс защищающегося героя");
                    return;
                }
                CheckPlayerClasses();
            }
            if (label16.Text == "БИТВА ЗАВЕРШЕНА")
            {
                MessageBox.Show("Битва завершена");
                return;
            }
            foreach (var leftBox in leftBoxs)
            {
                leftBox.BackColor = Color.White;
            }
            foreach (var rightBox in rightBoxs)
            {
                rightBox.BackColor = Color.White;
            }
            localLeftMonsters.RemoveAll(x => x.ToString().Length > 0);
            localRightMonsters.RemoveAll(x => x.ToString().Length > 0);
            //add leftMonsters objects to new LIST and update quantity on DB
            CreateNewListOfMonstersAndUpdateQuantity("left");
            //add rightMonsters objects to new LIST and update quantity on DB
            CreateNewListOfMonstersAndUpdateQuantity("right");

            //Sort by speed
            localLeftMonsters.Sort((x, y) => x.CompareTo(y.speed));
            localRightMonsters.Sort((x, y) => x.CompareTo(y.speed));

            int idLeft = GetIdOnTheLeft();

            if (idLeft == -1)
            {
                MessageBox.Show("Бой завершен, очистите стеки");
                label16.Text = "БИТВА ЗАВЕРШЕНА";
                return;
            }
            int idRight = GetIdOnTheRight();

            if (idRight == -1)
            {
                MessageBox.Show("Бой завершен, очистите стеки");
                label16.Text = "БИТВА ЗАВЕРШЕНА";
                return;
            }
            if (parityCount == 0)
                DetermineSide(idLeft, idRight);
            units localLeft = new units();
            units localRight = new units();
            if (parityCount == 1)
            {
                label16.Text = "Ход левой стороны";
                currentLeftTextBox = GetLeftUnitById(idLeft).currentLeftBox.GetValueOrDefault() - 1;
                if (rightTextBoxs[currentLeftTextBox].BackColor != Color.WhiteSmoke)
                    currentRightTextBox = currentLeftTextBox;
                else
                    currentRightTextBox = GetRightTextBox(currentLeftTextBox);
                int localIdLeft = leftMonsters[currentLeftTextBox];
                localLeft = GetLeftUnitById(localIdLeft);
                int localIdRight = rightMonsters[currentRightTextBox];
                localRight = GetRightUnitById(localIdRight);
            }
            else
            {
                label16.Text = "Ход правой стороны";
                currentRightTextBox = GetRightUnitById(idRight).currentRightBox.GetValueOrDefault() - 1;
                if (leftTextBoxs[currentRightTextBox].BackColor != Color.WhiteSmoke)
                    currentLeftTextBox = currentRightTextBox;
                else
                    currentLeftTextBox = GetLeftTextBox(currentRightTextBox);
                int localIdLeft = leftMonsters[currentLeftTextBox];
                localLeft = GetLeftUnitById(localIdLeft);
                int localIdRight = rightMonsters[currentRightTextBox];
                localRight = GetRightUnitById(localIdRight);
            }
            Random firstRandom;
            Random secondRandom;
            leftMonsterAttack = localLeft.attack;
            leftMonsterHP = localLeft.hp;
            leftMonsterMaxDamage = localLeft.maxDamage.GetValueOrDefault();
            leftMonsterDefence = localLeft.defence;
            currentHPOfLeftMonster = localLeft.hp;
            monstersRemaningOnLeft = localLeft.monstersRemainingOnLeft.GetValueOrDefault();
            remainingHPOnTheLeft = currentHPOfLeftMonster * monstersRemaningOnLeft;
            if (CalculateLuck(luckForLeft))
                leftMonsterAverageDamage = leftMonsterMaxDamage;
            else
            {
                firstRandom = new Random();
                leftMonsterMinDamage = localLeft.minDamage.GetValueOrDefault();
                leftMonsterAverageDamage = firstRandom.Next(leftMonsterMinDamage, leftMonsterMaxDamage + 1);
            }
            rightMonsterAttack = localRight.attack;
            rightMonsterHP = localRight.hp;
            rightMonsterMaxDamage = localRight.maxDamage.GetValueOrDefault();
            rightMonsterDefence = localRight.defence;
            currentHPOfRightMonster = localRight.hp;
            monstersRemaningOnRight = localRight.monstersRemainingOnRight.GetValueOrDefault();
            remainingHPOnTheRight = currentHPOfRightMonster * monstersRemaningOnRight;
            if (CalculateLuck(luckForRight))
                rightMonsterAverageDamage = rightMonsterMaxDamage;
            else
            {
                secondRandom = new Random();
                rightMonsterMinDamage = localRight.minDamage.GetValueOrDefault();
                rightMonsterAverageDamage = secondRandom.Next(rightMonsterMinDamage, rightMonsterMaxDamage + 1);       
            }
            
            //Game battle
            if (parityCount == 1)
            //ход левой стороны
            {
                leftBoxs[currentLeftTextBox].BackColor = Color.Green;
                rightBoxs[currentRightTextBox].BackColor = Color.Red;
                int coeffOfTotalDamage = CalculateChanceOfDoubleDamageForLeft(moraleForLeft, currentLeftTextBox);
                if (leftMonsterAttack >= rightMonsterDefence)
                    damage = coeffOfTotalDamage * coefficientForLeft * GetCoefficientOfAttack(localLeft.classOfMonster, localLeft.monsterSize.GetValueOrDefault(), localRight.classOfMonster, localRight.monsterSize.GetValueOrDefault()) * Convert.ToDouble((Math.Ceiling(monstersRemaningOnLeft) * leftMonsterAverageDamage) * (1 + (leftMonsterAttack - rightMonsterDefence) * 0.05));
                else
                    damage = coeffOfTotalDamage * coefficientForLeft * GetCoefficientOfAttack(localLeft.classOfMonster, localLeft.monsterSize.GetValueOrDefault(), localRight.classOfMonster, localRight.monsterSize.GetValueOrDefault()) * Convert.ToDouble((Math.Ceiling(monstersRemaningOnLeft) * leftMonsterAverageDamage) / (1 + (rightMonsterDefence - leftMonsterAttack) * 0.05));
                leftTextBoxs[currentLeftTextBox].BackColor = Color.Aquamarine;
                remainingHPOnTheRight -= damage;
                label6.Text += "\n" + step + ") " + localLeft.unitName + " ударил \nна " + Math.Round(damage,2).ToString()+ " урона " + localRight.unitName;
                monstersRemaningOnRight = remainingHPOnTheRight / rightMonsterHP;
                UpdateQuantityOnRight(localRight.idUnit, monstersRemaningOnRight);
                rightTextBoxs[currentRightTextBox].Text = Math.Ceiling(monstersRemaningOnRight).ToString();
                rightLabels[currentRightTextBox].Text = Math.Round(monstersRemaningOnRight, 2).ToString();
                if (monstersRemaningOnRight > 0)
                {
                    if (rightMonsterAttack >= leftMonsterDefence)
                        damage = coefficientForRight * GetCoefficientOfAttack(localRight.classOfMonster,localRight.monsterSize.GetValueOrDefault(), localLeft.classOfMonster, localLeft.monsterSize.GetValueOrDefault()) * Convert.ToDouble((Math.Ceiling(monstersRemaningOnRight) * rightMonsterAverageDamage) * (1 + (rightMonsterAttack - leftMonsterDefence) * 0.05));
                    else
                        damage = coefficientForRight * GetCoefficientOfAttack(localRight.classOfMonster,localRight.monsterSize.GetValueOrDefault(), localLeft.classOfMonster, localLeft.monsterSize.GetValueOrDefault()) * Convert.ToDouble((Math.Ceiling(monstersRemaningOnRight) * rightMonsterAverageDamage) / (1 + (leftMonsterDefence - rightMonsterAttack) * 0.05));
                    remainingHPOnTheLeft -= damage;
                    label8.Text += "\n" + step + ") " + localRight.unitName + " ответил \nна " + Math.Round(damage, 2).ToString() + " урона " + localLeft.unitName;
                    monstersRemaningOnLeft = remainingHPOnTheLeft / leftMonsterHP;
                    UpdateQuantityOnLeft(localLeft.idUnit, monstersRemaningOnLeft);
                    leftTextBoxs[currentLeftTextBox].Text = Math.Ceiling(monstersRemaningOnLeft).ToString();
                    leftLabels[currentLeftTextBox].Text = Math.Round(monstersRemaningOnLeft, 2).ToString();
                    if (monstersRemaningOnLeft <= 0)
                    {
                        label8.Text += "\n " + localLeft.unitName+ " убит";
                        leftTextBoxs[currentLeftTextBox].BackColor = Color.WhiteSmoke;
                    }
                }
                else
                {
                    label6.Text += "\n " + localRight.unitName +" убит";
                    rightTextBoxs[currentRightTextBox].BackColor = Color.WhiteSmoke;
                }
                label16.Text = "Ход левой стороны";
            }
            //ход правой стороны
            else
            {
                leftBoxs[currentLeftTextBox].BackColor = Color.Red;
                rightBoxs[currentRightTextBox].BackColor = Color.Green;
                int coeffOfTotalDamage = CalculateChanceOfDoubleDamageForRight(moraleForRight, currentRightTextBox);
                if (rightMonsterAttack >= leftMonsterDefence)
                    damage = coeffOfTotalDamage * coefficientForRight * GetCoefficientOfAttack(localRight.classOfMonster,localRight.monsterSize.GetValueOrDefault(), localLeft.classOfMonster, localLeft.monsterSize.GetValueOrDefault()) * Convert.ToDouble((Math.Ceiling(monstersRemaningOnRight) * rightMonsterAverageDamage) * (1 + (rightMonsterAttack - leftMonsterDefence) * 0.05));
                else
                    damage = coeffOfTotalDamage * coefficientForRight * GetCoefficientOfAttack(localRight.classOfMonster,localRight.monsterSize.GetValueOrDefault(), localLeft.classOfMonster,localLeft.monsterSize.GetValueOrDefault()) * Convert.ToDouble((Math.Ceiling(monstersRemaningOnRight) * rightMonsterAverageDamage) / (1 + (leftMonsterDefence - rightMonsterAttack) * 0.05));
                remainingHPOnTheLeft -= damage ;

                rightTextBoxs[currentRightTextBox].BackColor = Color.Aquamarine;
                label8.Text += "\n" + step + ") " + localRight.unitName + " ударил \nна " + Math.Round(damage, 2).ToString() + " урона " + localLeft.unitName;
                monstersRemaningOnLeft = remainingHPOnTheLeft / leftMonsterHP;
                UpdateQuantityOnLeft(localLeft.idUnit, monstersRemaningOnLeft);
                leftTextBoxs[currentLeftTextBox].Text = Math.Ceiling(monstersRemaningOnLeft).ToString();
                leftLabels[currentLeftTextBox].Text = Math.Round(monstersRemaningOnLeft, 2).ToString();
                if (monstersRemaningOnLeft > 0)
                {
                    if (leftMonsterAttack >= rightMonsterDefence)
                        damage = coefficientForLeft * GetCoefficientOfAttack(localLeft.classOfMonster,localLeft.monsterSize.GetValueOrDefault(), localRight.classOfMonster, localRight.monsterSize.GetValueOrDefault()) * Convert.ToDouble((Math.Ceiling(monstersRemaningOnLeft) * leftMonsterAverageDamage) * (1 + (leftMonsterAttack - rightMonsterDefence) * 0.05));
                    else
                        damage = coefficientForLeft * GetCoefficientOfAttack(localLeft.classOfMonster,localLeft.monsterSize.GetValueOrDefault(), localRight.classOfMonster, localRight.monsterSize.GetValueOrDefault()) * Convert.ToDouble((Math.Ceiling(monstersRemaningOnLeft) * leftMonsterAverageDamage) / (1 + (rightMonsterDefence - leftMonsterAttack) * 0.05));
                    remainingHPOnTheRight -= damage;
                    label6.Text += "\n" + step + ") " + localLeft.unitName + " ответил \nна" + Math.Round(damage, 2).ToString() + " урона " + localRight.unitName;
                    monstersRemaningOnRight = remainingHPOnTheRight / rightMonsterHP;
                    UpdateQuantityOnRight(localRight.idUnit, monstersRemaningOnRight);
                    rightTextBoxs[currentRightTextBox].Text = Math.Ceiling(monstersRemaningOnRight).ToString();
                    rightLabels[currentRightTextBox].Text = Math.Round(monstersRemaningOnRight, 2).ToString();
                    if (monstersRemaningOnRight <= 0)
                    {
                        label6.Text += " \n " + localRight.unitName + " убит";
                        rightTextBoxs[currentRightTextBox].BackColor = Color.WhiteSmoke;
                    }
                }
                else
                {
                    label8.Text += "\n " +localLeft.unitName+" убит";
                    leftTextBoxs[currentLeftTextBox].BackColor = Color.WhiteSmoke;
                }
                label16.Text = "Ход правой стороны";
            }
            parityCount = 0;
            step++;
        }

        //change stats
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox15.Text == "")
            {
                MessageBox.Show("Заполните количество атаки");
                return;
            }
            if (textBox16.Text == "")
            {
                MessageBox.Show("Заполните количество HP");
                return;
            }
            if (textBox17.Text == "")
            {
                MessageBox.Show("Заполните количество защиты");
                return;
            }
            if (textBox18.Text == "")
            {
                MessageBox.Show("Заполните скорость");
                return;
            }
            if (textBox19.Text == "")
            {
                MessageBox.Show("Заполните количество минимального дамажа");
                return;
            }
            if (textBox20.Text == "")
            {
                MessageBox.Show("Заполните количество максимального дамажа");
                return;
            }
            units localUnit = GetUnitByName(listBox1.GetItemText(listBox1.SelectedItem));
            int localId = localUnit.idUnit;

            units unit = db.units.Find(localId);
            unit.idUnit = localId;
            unit.attack = Int32.Parse(textBox15.Text);
            unit.hp = Int32.Parse(textBox16.Text);
            unit.defence = Int32.Parse(textBox17.Text);
            unit.speed = Int32.Parse(textBox18.Text);
            unit.minDamage = Int32.Parse(textBox19.Text);
            unit.maxDamage = Int32.Parse(textBox20.Text);
            unit.unitName = listBox1.GetItemText(listBox1.SelectedItem);

            db.Entry(unit).State = EntityState.Modified;
            db.SaveChanges();

            MessageBox.Show("Статы обновлены");
        }
        
        private void button39_Click(object sender, EventArgs e)
        {
            foreach (var l in leftLabels)
            {
                l.Text = " ";
            }

            coefficientForLeft = 1.0;
            coefficientForRight = 1.0;
            parityCount = 0;
            label6.Text = "";
            label8.Text = "";
            step = 1;
            foreach (var leftBox in leftRadioButtons)
            {
                leftBox.Checked = false;
            }
            foreach (var leftBox in leftBoxs)
            {
                leftBox.BackColor = Color.White;
            }
            foreach (var a in leftTextBoxs)
            {
                a.Enabled = true;
            }
            foreach (var a in leftTextBoxs)
            {
                a.Text = "";
            }
            foreach (var a in leftTextBoxs)
            {
                a.BackColor = Color.White;
            }
            foreach (var a in leftBoxs)
            {
                a.Image = null;
            }
            foreach (units u in db.units)
            {
                LeftBoxsValueToNull(u);
            }
            db.SaveChanges();
            label16.Text = "ДОБАВЬТЕ МОНСТРОВ И НАЖМИТЕ \"СЛЕД. ШАГ\"";
            currentLeftTextBox = -1;
            currentRightTextBox = -1;
            leftMonsters.RemoveAll(x => x.ToString().Length > 0);
            localLeftMonsters.RemoveAll(x => x.ToString().Length > 0);
        }

        private void LeftBoxsValueToNull(units u)
        {
            units unit = db.units.Find(u.idUnit);
            unit.currentLeftBox = null;
            db.Entry(unit).State = EntityState.Modified;

        }
        private void RightBoxsValueToNull(units u)
        {
            units unit = db.units.Find(u.idUnit);
            unit.currentRightBox = null;
            db.Entry(unit).State = EntityState.Modified;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var unit in db.units)
            {
                if (unit.idUnit == 999)
                    continue;
                listBox1.Items.Add(unit.unitName);
            }
            for (int i = 3; i>=0; i--)
            {
                domainUpDown1.Items.Add(i);
                domainUpDown2.Items.Add(i);
            }
            
        }

        private void button46_Click(object sender, EventArgs e)
        {
            foreach(var l in rightLabels)
            {
                l.Text = " ";
            }
            coefficientForLeft = 1.0;
            coefficientForRight = 1.0;
            parityCount = 0;
            label6.Text = "";
            label8.Text = "";
            step = 1;
            foreach (var rightBox in rightRadioButtons)
            {
                rightBox.Checked = false;
            }
            foreach (var rightBox in rightBoxs)
            {
                rightBox.BackColor = Color.White;
            }
            foreach (var a in rightTextBoxs)
            {
                a.Text = "";
            }
            foreach (var a in rightTextBoxs)
            {
                a.Enabled = true;

            }
            foreach (var a in rightTextBoxs)
            {
                a.BackColor = Color.White;
            }
            foreach (var a in rightBoxs)
            {
                a.Image = null;
            }

            foreach (units u in db.units)
            {
                RightBoxsValueToNull(u);
            }
            db.SaveChanges();
            label16.Text = "ДОБАВЬТЕ МОНСТРОВ И НАЖМИТЕ \"СЛЕД ШАГ\"";
            currentLeftTextBox = -1;
            currentRightTextBox = -1;
            rightMonsters.RemoveAll(x => x.ToString().Length > 0);
            localRightMonsters.RemoveAll(x => x.ToString().Length > 0);
        }

        private void button40_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < leftBoxs.Length; i++)
            {
                if (leftBoxs[i].Image == null)
                {
                    leftBoxs[i].Image = imageList1.Images[0];
                    leftTextBoxs[i].Enabled = false;
                    leftTextBoxs[i].BackColor = Color.WhiteSmoke;
                    leftMonsters.Add(999);
                    break;
                }
            }
        }

        private void button41_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < rightBoxs.Length; i++)
            {
                if (rightBoxs[i].Image == null)
                {
                    rightBoxs[i].Image = imageList1.Images[0];
                    rightTextBoxs[i].Enabled = false;
                    rightTextBoxs[i].BackColor = Color.WhiteSmoke;
                    rightMonsters.Add(999);
                    break;
                }
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                listBox1.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                label21.Visible = true;
                label22.Visible = true;
                label23.Visible = true;
                label24.Visible = true;
                label25.Visible = true;
                label9.Visible = true;
                label7.Visible = true;
                label5.Visible = true;
                label4.Visible = true;
                label2.Visible = true;
                label26.Visible = true;
                label27.Visible = true;
                label28.Visible = true;
                textBox15.Visible = true;
                textBox16.Visible = true;
                textBox17.Visible = true;
                textBox18.Visible = true;
                textBox19.Visible = true;
                textBox20.Visible = true;
                button166.Visible = true;
            }

            else
            {
                listBox1.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
                label13.Visible = false;
                label14.Visible = false;
                label21.Visible = false;
                label22.Visible = false;
                label23.Visible = false;
                label24.Visible = false;
                label25.Visible = false;
                label26.Visible = false;
                label27.Visible = false;
                label28.Visible = false;
                label7.Visible = false;
                label9.Visible = false;
                label5.Visible = false;
                label4.Visible = false;
                label2.Visible = false;
                textBox15.Visible = false;
                textBox16.Visible = false;
                textBox17.Visible = false;
                textBox18.Visible = false;
                textBox19.Visible = false;
                textBox20.Visible = false;
                button166.Visible = false;
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentName = listBox1.GetItemText(listBox1.SelectedItem);
            label21.Text = GetUnitByName(currentName).attack.ToString();
            label22.Text = GetUnitByName(currentName).hp.ToString();
            label23.Text = GetUnitByName(currentName).defence.ToString();
            label24.Text = GetUnitByName(currentName).speed.ToString();
            label25.Text = GetUnitByName(currentName).minDamage.GetValueOrDefault().ToString();
            label27.Text = GetUnitByName(currentName).maxDamage.GetValueOrDefault().ToString();
        }
        
        //varvar
        private void button4_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 1) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(1);
                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button4.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button4.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 1) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(1);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button4.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button4.BackgroundImage;
                        break;
                    }

                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 2) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(2);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button5.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button5.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 2) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(2);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button5.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button5.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 3) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(3);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button6.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button6.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 3) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(3);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button6.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button6.BackgroundImage;
                        break;
                    }

                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 4) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(4);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button7.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button7.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 4) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(4);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button7.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button7.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 5) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(5);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button8.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button8.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 5) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(5);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button8.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button8.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 6) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(6);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button9.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button9.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 6) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(6);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button9.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button9.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 7) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(7);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button10.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button10.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 7) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(7);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button10.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button10.BackgroundImage;
                        break;
                    }

                }
            }
        }


        //mage
        private void button11_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 15) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(15);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button11.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button11.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 15) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(15);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button11.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button11.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 16) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(16);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button12.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button12.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 16) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(16);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button12.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button12.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 17) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(17);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button13.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button13.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 17) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(17);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button13.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button13.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 18) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(18);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button14.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button14.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 18) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(18);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button14.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button14.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 19) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(19);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button15.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button15.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 19) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(19);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button15.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button15.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 20) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(20);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button16.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button16.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 20) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(20);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button16.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button16.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 21) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(21);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button17.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button17.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 21) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(21);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button17.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button17.BackgroundImage;
                        break;
                    }

                }
            }
        }



        //necromant

        private void button18_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 22) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(22);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button18.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button18.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 22) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(22);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button18.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button18.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 23) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(23);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button19.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button19.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 23) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(23);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button19.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button19.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 24) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(24);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button20.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button20.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 24) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(24);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button20.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button20.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 25) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(25);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button21.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button21.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 25) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(25);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button21.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button21.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 26) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(26);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button22.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button22.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 26) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(26);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button22.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button22.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 27) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(27);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button23.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button23.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 27) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(27);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button23.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button23.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 28) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(28);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button24.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button24.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 28) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(28);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button24.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button24.BackgroundImage;
                        break;
                    }

                }
            }
        }


        

        //paladin
        private void button35_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 8) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(8);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button35.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button35.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 8) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(8);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button35.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button35.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button34_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 9) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(9);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button34.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button34.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 9) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(9);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button34.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button34.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button33_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 10) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(10);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button33.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button33.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 10) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(10);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button33.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button33.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button32_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 11) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(11);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button32.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button32.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 11) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(11);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button32.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button32.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button31_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 12) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(12);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button31.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button31.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 12) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(12);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button31.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button31.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button30_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 13) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(13);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button30.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button30.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 13) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(13);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button30.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button30.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button29_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 14) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(14);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button29.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button29.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 14) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(14);

                int local = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button29.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button29.BackgroundImage;
                        break;
                    }

                }
            }
        }

        //razboy

        private void button28_Click_1(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 29) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(29);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button28.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button28.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 29) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(29);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button28.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button28.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button27_Click_1(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 30) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(30);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button27.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button27.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 30) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(30);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button27.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button27.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button26_Click_1(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 31) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(31);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button26.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button26.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 31) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(31);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button26.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button26.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button252_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 32) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(32);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button252.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button252.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 32) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(32);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button252.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button252.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button242_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 33) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(33);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button242.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button242.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 33) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(33);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button242.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button242.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button232_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 34) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(34);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button232.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button232.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 34) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(34);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button232.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button232.BackgroundImage;
                        break;
                    }

                }
            }
        }

        private void button222_Click(object sender, EventArgs e)
        {
            int local = 0;
            if (radioButton1.Checked)
            {
                if (leftMonsters != null)
                {
                    for (int i = 0; i <= leftMonsters.Count - 1; i++)
                    {
                        if ((leftMonsters[i] == 35) || (leftMonsters.Count == 7))
                            return;
                    }
                }
                leftMonsters.Add(35);


                for (int i = 0; i <= 6; i++)
                {
                    if (leftBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (leftBoxs[i].Image != null)
                    {
                        leftBoxs[local].Image = button222.BackgroundImage;
                        break;
                    }

                    else
                    {
                        leftBoxs[i].Image = button222.BackgroundImage;
                        break;
                    }

                }
            }
            if (radioButton2.Checked)
            {
                if (rightMonsters != null)
                {
                    for (int i = 0; i <= rightMonsters.Count - 1; i++)
                    {
                        if ((rightMonsters[i] == 35) || (rightMonsters.Count == 7))
                            return;
                    }
                }
                rightMonsters.Add(35);

                for (int i = 0; i <= 6; i++)
                {
                    if (rightBoxs[i].Image == null)
                    {
                        local = i;
                        break;
                    }
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (i == 6) break;
                    else if (rightBoxs[i].Image != null)
                    {
                        rightBoxs[local].Image = button222.BackgroundImage;
                        break;
                    }

                    else
                    {
                        rightBoxs[i].Image = button222.BackgroundImage;
                        break;
                    }

                }
            }
        }

    }
}
