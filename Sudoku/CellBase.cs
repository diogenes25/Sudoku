﻿using DE.ONNEN.Sudoku.SudokuExternal;
using System.Collections.Generic;
using System.ComponentModel;

namespace DE.ONNEN.Sudoku
{
	public abstract class CellBase : ICellBase, INotifyPropertyChanged
	{
		/// <summary>
		/// Internal candidateValue to set value without NotifyPropertyChanged-Event.
		/// </summary>
		protected int candidateValue = 0;

		private int id;

		//private HouseType houseType;

		public event PropertyChangedEventHandler PropertyChanged;

		/// <inheritdoc />
		public HouseType HType
		{
			get;
			protected set;
		}

		/// <inheritdoc />
		public int ID
		{
			get { return this.id; }
			protected set { this.id = value; }
		}

		/// <inheritdoc />
		public int CandidateValue
		{
			get { return this.candidateValue; }
			internal set { SetField(ref this.candidateValue, value, "CandidateValue"); }
		}

		internal abstract bool SetDigit(int digit, SudokuLog sudokuResult);

		/// <summary>
		/// Cell-Value (CandidateValue and/or Digit) changed.
		/// </summary>
		/// <param name="propertyName">Digit or CadidateValue</param>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// Set new PropertyValue and fire ProperyChanged-Event when old value differs from new value.
		/// </summary>
		/// <typeparam name="T">Propertytype</typeparam>
		/// <param name="field">Property</param>
		/// <param name="value">new value</param>
		/// <param name="propertyName">Propertyname</param>
		/// <returns></returns>
		protected bool SetField<T>(ref T field, T value, string propertyName)
		{
			if (EqualityComparer<T>.Default.Equals(field, value)) return false;
			field = value;
			OnPropertyChanged(propertyName);
			return true;
		}
	}
}