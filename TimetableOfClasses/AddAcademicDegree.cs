﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibOfTimetableOfClasses;

namespace TimetableOfClasses
{
	public partial class AddAcademicDegree : Form
	{
		public AddAcademicDegree()
		{
			InitializeComponent();
			update = false;
		}
		bool update = false;

		public AddAcademicDegree(MAcademicDegree mAcademicDegree)
		{
			InitializeComponent();
			this.Text = "Изменение учёной степени";
			this.button1.Visible = false;
			this.button2.Text = "Изменить";
			this.Reduction.Enabled = false;
			FullName.Text = mAcademicDegree.FullName;
			Reduction.Text = mAcademicDegree.Reduction;
			update = true;
		}

		private void Button1_Click(object sender, EventArgs e)  // Создать и очистить
		{
			if ((Reduction.Text.Length != 0) || (FullName.Text.Length != 0))
			{
				if (Reduction.Text.Length != 0)
				{
					if (FullName.Text.Length != 0)
					{
						try
						{
							MAcademicDegree AcademicDegree = new MAcademicDegree(FullName.Text, Reduction.Text);
							RefData.CAcademicDegree.Insert(AcademicDegree);
							FullName.Text = "";
							Reduction.Text = "";
						}
						catch (Exception)
						{
							MessageBox.Show("Некорректно заполнены поля", "Ошибка");
						}
					}
					else MessageBox.Show("Заполните полe 'Полная запись учёной степени'", "Попробуйте снова", MessageBoxButtons.OK);
				}
				else MessageBox.Show("Заполните полe 'Сокращённая запись учёной степени'", "Попробуйте снова", MessageBoxButtons.OK);
			}
			else MessageBox.Show("Заполните поля", "Попробуйте снова", MessageBoxButtons.OK);
		}
			private void Button2_Click(object sender, EventArgs e) //Создать и закрыть
		{
			if (update)
			{
				if ((Reduction.Text.Length != 0) || (FullName.Text.Length != 0))
				{
					if (Reduction.Text.Length != 0)
					{
						if (FullName.Text.Length != 0)
						{
							try
							{
								MAcademicDegree AcademicDegree = new MAcademicDegree(FullName.Text, Reduction.Text);
								RefData.CAcademicDegree.Update(AcademicDegree);
								FullName.Text = "";
								Reduction.Text = "";
								Close();
							}
							catch (Exception)
							{
								MessageBox.Show("Некорректно заполнены поля", "Ошибка");
							}
							
						}
						else MessageBox.Show("Заполните полe 'Полная запись учёной степени'", "Попробуйте снова", MessageBoxButtons.OK);
					}
					else MessageBox.Show("Заполните полe 'Сокращённая запись учёной степени'", "Попробуйте снова", MessageBoxButtons.OK);
				}
				else MessageBox.Show("Заполните поля", "Попробуйте снова", MessageBoxButtons.OK);
			}
			else
{

				if ((Reduction.Text.Length != 0) || (FullName.Text.Length != 0))
				{
					if (Reduction.Text.Length != 0)
					{
						if (FullName.Text.Length != 0)
						{
							try
							{
								MAcademicDegree AcademicDegree = new MAcademicDegree(FullName.Text, Reduction.Text);
								RefData.CAcademicDegree.Insert(AcademicDegree);
								FullName.Text = "";
								Reduction.Text = "";
								Close();
							}
							catch (Exception)
							{
								MessageBox.Show("Некорректно заполнены поля", "Ошибка");
							}
						}
						else MessageBox.Show("Заполните полe 'Полная запись учёной степени'", "Попробуйте снова", MessageBoxButtons.OK);
					}
					else MessageBox.Show("Заполните полe 'Сокращённая запись учёной степени'", "Попробуйте снова", MessageBoxButtons.OK);
				}
				else MessageBox.Show("Заполните поля", "Попробуйте снова", MessageBoxButtons.OK);
			}
		}

		private void Button3_Click(object sender, EventArgs e) //Отмена
		{
			this.Close();
		}

		private void Reduction_KeyPress(object sender, KeyPressEventArgs e)
		{
			char l = e.KeyChar;
			if ((l < 'А' || l > 'я') && l != '\b' && l != '.' && l != '-')
			{
				e.Handled = true;
			}
		}

		private void FullName_KeyPress(object sender, KeyPressEventArgs e)
		{
			char l = e.KeyChar;
			if ((l < 'А' || l > 'я') && l != '\b' && l != '.' && l != '-')
			{
				e.Handled = true;
			}
		}

		private void Reduction_TextChanged(object sender, EventArgs e)
		{
			if (((TextBox)sender).Text.Length == 1)
				((TextBox)sender).Text = ((TextBox)sender).Text.ToUpper();
			((TextBox)sender).Select(((TextBox)sender).Text.Length, 0);
		}

		private void FullName_TextChanged(object sender, EventArgs e)
		{
			if (((TextBox)sender).Text.Length == 1)
				((TextBox)sender).Text = ((TextBox)sender).Text.ToUpper();
			((TextBox)sender).Select(((TextBox)sender).Text.Length, 0);
		}
		private static string PeriodLetterToUpper(string str)
		{
			if (str.Length > 0)
			{
				if (str.IndexOf(",") > 0)
				{
					char p;
					str = Char.ToUpper(str[0]) + str.Substring(1);
					for (int i = 0; i < str.Length; i++)
					{
						if (str[i] == ',')
						{
							p = Char.ToUpper(str[i + 2]);
							str = str.Remove(i + 2, 1);
							str = str.Insert(i + 2, "" + p);
						}
					}
					return str;
				}
				else
					return Char.ToUpper(str[0]) + str.Substring(1);
			}
			return "";
		}
		private void Reduction_Leave(object sender, EventArgs e)
		{
			TextBox R = sender as TextBox;
			R.Text = Regex.Replace(R.Text, "[^а-яА-Я ]", "");
			R.Text = Regex.Replace(R.Text, "[, ]+", ", ");

			if (R.Text.Length > 2)
			{
				if (R.Text.IndexOf(", ") == 0)
					R.Text = R.Text.Substring(1);
				if (R.Text.LastIndexOf(", ") == R.Text.Length - 1)
					R.Text = R.Text.Remove(R.Text.Length - 1);
				R.Text = R.Text.ToLower();
				R.Text = PeriodLetterToUpper(R.Text);
			}
		}

		private void FullName_Leave(object sender, EventArgs e)
		{
			TextBox R = sender as TextBox;
			R.Text = Regex.Replace(R.Text, "[^а-яА-Я ]", "");
			R.Text = Regex.Replace(R.Text, "[, ]+", ", ");

			if (R.Text.Length > 2)
			{
				if (R.Text.IndexOf(", ") == 0)
					R.Text = R.Text.Substring(1);
				if (R.Text.LastIndexOf(", ") == R.Text.Length - 1)
					R.Text = R.Text.Remove(R.Text.Length - 1);
				R.Text = R.Text.ToLower();
				R.Text = PeriodLetterToUpper(R.Text);
			}
		}
	}
}

