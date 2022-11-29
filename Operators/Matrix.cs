﻿using System;
using System.Text;
using System.Globalization;

namespace Lab07 {
    /**
     * Class representing matrix mxn, ie. m-rows and n-columns
     */
    public class Matrix {
        #region STAGE_1

        private readonly double[,] _array;

        public int M => _array.GetLength(0);
        public int N => _array.GetLength(1);

        public double this[Index i, Index j] {
            get => _array[i.GetOffset(M), j.GetOffset(N)];
            set => _array[i.GetOffset(M), j.GetOffset(N)] = value;
        }

        public Matrix this[Index m, Range n] {
            get => this[m.GetOffset(M)..(m.GetOffset(M) + 1), n];
            set => this[m.GetOffset(M)..(m.GetOffset(M) + 1), n] = value;
        }

        public Matrix this[Range m, Index n] {
            get => this[m, n.GetOffset(N)..(n.GetOffset(N) + 1)];
            set => this[m, n.GetOffset(N)..(n.GetOffset(N) + 1)] = value;
        }

        public Matrix this[Range m, Range n] {
            get {
                (int mOffset, int mLength) = m.GetOffsetAndLength(M);
                (int nOffset, int nLength) = n.GetOffsetAndLength(N);
                Matrix matrix = new Matrix(mLength, nLength);
                for (int i = mOffset; i < mOffset + mLength; i++)
                    for (int j = nOffset; j < nOffset + nLength; j++)
                        matrix[i - mOffset, j - nOffset] = this[i, j];
                return matrix;
            }
            set {
                (int mOffset, int mLength) = m.GetOffsetAndLength(M);
                (int nOffset, int nLength) = n.GetOffsetAndLength(N);
                for (int i = mOffset; i < mOffset + mLength; i++)
                    for (int j = nOffset; j < nOffset + nLength; j++)
                        this[i, j] = value[i - mOffset, j - nOffset];
            }
        }

        public Matrix(double[,] array) {
            int m = array.GetLength(0);
            int n = array.GetLength(1);
            _array = new double[m, n];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    _array[i, j] = array[i, j];
        }

        public Matrix(int m, int n) {
            _array = new double[m, n];
        }

        #endregion

        #region STAGE_2

        public static explicit operator double(Matrix value) {
            if (value.M != 1 || value.N != 1) throw new InvalidCastException();
            return value[0, 0];
        }

        public static implicit operator Matrix(double value) {
            return new Matrix(new[,] { { value } });
        }

        public static explicit operator double[,](Matrix value) {
            double[,] array = new double[value.M, value.N];
            for (int i = 0; i < value.M; i++)
                for (int j = 0; j < value.N; j++)
                    array[i, j] = value._array[i, j];

            return array;
        }

        public static implicit operator Matrix(double[,] array) {
            return new Matrix(array);
        }

        protected bool Equals(Matrix other) {
            if (M != other.M || N != other.N) return false;
            for (int i = 0; i < M; i++)
                for (int j = 0; j < N; j++)
                    if (this[i, j] != other[i, j])
                        return false;
            return true;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Matrix)obj);
        }

        public override int GetHashCode() {
            return (_array != null ? _array.GetHashCode() : 0);
        }

        public static bool operator ==(Matrix left, Matrix right) {
            return Equals(left, right);
        }

        public static bool operator !=(Matrix left, Matrix right) {
            return !Equals(left, right);
        }

        #endregion

        #region STAGE_3

        public static Matrix operator +(Matrix a, Matrix b) {
            if (a.M != b.M || a.N != b.N) throw new ArgumentException();
            Matrix result = new Matrix(a.M, a.N);
            for (int i = 0; i < a.M; i++)
                for (int j = 0; j < a.N; j++)
                    result[i, j] = a[i, j] + b[i, j];

            return result;
        }

        public static Matrix operator +(Matrix a) {
            return new Matrix(a._array);
        }

        public static Matrix operator -(Matrix a) {
            Matrix result = new Matrix(a.M, a.N);
            for (int i = 0; i < a.M; i++)
                for (int j = 0; j < a.N; j++)
                    result[i, j] = -a[i, j];

            return result;
        }

        public static Matrix operator -(Matrix a, Matrix b) {
            if (a.M != b.M || a.N != b.N) throw new ArgumentException();
            Matrix result = new Matrix(a.M, a.N);
            for (int i = 0; i < a.M; i++)
                for (int j = 0; j < a.N; j++)
                    result[i, j] = a[i, j] - b[i, j];

            return result;
        }

        public static Matrix operator !(Matrix a) {
            Matrix result = new Matrix(a.N, a.M);
            for (int i = 0; i < a.M; i++)
                for (int j = 0; j < a.N; j++)
                    result[j, i] = a[i, j];

            return result;
        }

        public static Matrix operator |(Matrix a, Matrix b) {
            if (a.M != b.M) throw new ArgumentException();

            Matrix result = new Matrix(a.M, a.N + b.N);
            result[.., ..a.N] = a;
            result[.., a.N..] = b;
            return result;
        }

        #endregion

        #region STAGE_4

        public static Matrix operator *(Matrix a, double b) {
            Matrix result = new Matrix(a.M, a.N);
            for (int i = 0; i < a.M; i++)
                for (int j = 0; j < a.N; j++)
                    result[i, j] = a[i, j] * b;

            return result;
        }

        public static Matrix operator /(Matrix a, Matrix b) {
            if (a.M != a.N || b.M != a.M) throw new ArgumentException();
            Matrix result = a | b;
            for (int i = 0; i < result.M; i++) {
                int pivot = -1;
                double max = Double.NegativeInfinity;
                for (int j = i; j < result.M; j++) {
                    if (max < Math.Abs(result[j, i])) {
                        pivot = j;
                        max = Math.Abs(result[j, i]);
                    }
                }

                if (max == 0) throw new Exception();

                (result[pivot, ..], result[i, ..]) = (result[i, ..], result[pivot, ..]);
                result[i, ..] /= result[i, i];

                for (int j = 0; j < result.M; j++) {
                    if (j == i) continue;
                    result[j, ..] -= result[i, ..] * result[j, i];
                }
            }

            return result[.., a.N..];
        }

        public static Matrix operator /(Matrix a, double b) {
            if (b == 0) throw new DivideByZeroException();

            Matrix result = new Matrix(a.M, a.N);
            for (int i = 0; i < a.M; i++)
                for (int j = 0; j < a.N; j++)
                    result[i, j] = a[i, j] / b;

            return result;
        }

        public static Matrix operator *(double b, Matrix a) => a * b;

        public static Matrix operator *(Matrix a, Matrix b) {
            if (a.N != b.M) throw new ArgumentException();
            Matrix result = new Matrix(a.M, b.N);
            for (int i = 0; i < a.M; i++) {
                for (int j = 0; j < b.M; j++) {
                    result[i, j] = 0;
                    for (int k = 0; k < a.N; k++) {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return result;
        }

        #endregion

        public override string ToString() {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < M; i++) {
                builder.Append('|');
                for (int j = 0; j < N; j++) {
                    builder.Append($" {this[i, j],5:0.0} ");
                }
                builder.Append('|');
                if (i != M - 1) builder.Append('\n');
            }

            return builder.ToString();
        }
    }
}