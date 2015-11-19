using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApplication1
{

    [Serializable]
    public class LengthNotMatchException : Exception
    {
        public LengthNotMatchException() { }
        public LengthNotMatchException(string message) : base(message) { }
        public LengthNotMatchException(string message, Exception inner) : base(message, inner) { }
        protected LengthNotMatchException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
    [Serializable]
    public class Vector:IComparable<Vector>
    {
        public int length;
        public double mathLength;
        public double[] vector;
        public Vector()
        {

        }
        public Vector(int count)
        {
            length = count;
            vector = new double[count];
        }
        public void Initialize(double num)
        {
            for (int i = 0; i < length; i++)
            {
                vector[i] = num;
            }
        }
        
        public double this[int num]
        {
            get { return vector[num]; }
            set { vector[num] = value; }
        }

        public override int GetHashCode()
        {
            return (int)length ^ (int)vector[0];
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Vector))
                return false;
            return Equals((Vector)obj);
        }
        public bool Equals(Vector other)
        {

            if (this.length == other.length)
            {
                for (int i = 0; i < this.length; i++)
                {
                    if (this[i] != other[i])
                        return false;
                }
                return true;
            }
            return false;
        }

        /*public static bool operator == (Vector op1, Vector op2) {
            return op1.Equals(op2);
        }
        public static bool operator !=(Vector op1, Vector op2)
        {
            return !op1.Equals(op2);
        }*/
        public int CompareTo(Vector other)
        {
           return this.mathLength.CompareTo(other.mathLength);   
        }
        public static Vector operator +(Vector op1, Vector op2)
        {

            if (op1.length != op2.length)
                throw new LengthNotMatchException("Length of vectors doesn't match");
            Vector vector = new Vector(op1.length);
            for (int i = 0; i < op1.length; i++)
            {
                vector[i] = op1[i] + op2[i];
            }
            return vector;
        }
        public static Vector operator *(Vector op1, Vector op2)
        {
            if (op1.length != op2.length)
                throw new LengthNotMatchException("Length of vectors doesn't match");
            Vector vector = new Vector(op1.length);
            for (int i = 0; i < op1.length; i++)
            {
                vector[i] = op1[i] * op2[i];
            }
            return vector;
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < length; i++)
            {
                str += vector[i] + ",";
            }
            str = str.Remove(str.Length - 1, 1);
            return str;
        }
        public void StrToVector(string str)
        {
            int k = 0;
            string temp = "";
            for (int i = 0; i < str.Length; i++)
            {
                temp += str[i];
                if (str[i] == ',')
                {
                    temp = temp.Remove(temp.Length - 1, 1);
                    vector[k] = Convert.ToDouble(temp);
                    k++;  
                    temp = "";
                }
            }
            vector[k] = Convert.ToDouble(temp);
        }

    }

    public class Cells
    {
        public double value;
        public Cells() {
            value = 0;
        }
        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

    }
    [Serializable]
    public class Matrix : IEnumerable<Vector>
    {
        public Vector[] matrix;
        public int rows;
        public int columns;
        
       // public int rowIndex { get; set; }
        public Matrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
           // rowIndex = -1;
            matrix = new Vector[rows];
            for (int i = 0; i < rows; i++)
            {
                Vector vect = new Vector(columns);
                matrix[i] = vect;
            }
        }
        public Matrix()
        {

        }
        public void Initialize(double digit)
        {

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i].vector[j] = digit;
                }
                
            }
        }
        public void FindLengthOfVectors()
        {
            double temp;
            foreach(Vector vect in matrix)
            {
                temp = 0;
                for (int i = 0; i < columns; i++)
                {
                    temp += Math.Pow(vect.vector[i], 2);
                }
                vect.mathLength = (Math.Sqrt(temp));
            }
        }


        public Vector this[int row]
        {
            get { return matrix[row]; }
            set { matrix[row] = value; }
        }

        public double this[int row, int column]
        {
            get { return matrix[row].vector[column]; }
            set { matrix[row].vector[column] = value; }
        }


        public bool Contains(Vector vector)
        {
            for (int i = 0; i < rows; i++)
            {

                if (vector == matrix[i])
                    return true;
            }
            return false;
        }

        public static Matrix operator +(Matrix op1, Matrix op2)
        {
            if ((op1.columns != op2.columns) && (op1.rows != op2.rows))
                throw new LengthNotMatchException("Length of matrix doesn't match");
            Matrix matrix = new Matrix(op1.rows, op1.columns);
            for (int i = 0; i < op1.rows; i++)
            {
                matrix[i] = op1[i] + op2[i];
            }
            return matrix;
        }
        public static Matrix operator *(Matrix op1, Matrix op2)
        {
            if (op1.columns != op2.rows)
                throw new LengthNotMatchException("Columns of first operand must be equals to rows of second operand");
            Matrix matrix = new Matrix(op1.rows, op2.columns);
            for (int i = 0; i < op1.rows; i++)
            {
                for (int j = 0; j < op2.columns; j++)
                {
                    for (int k = 0; k < op2.rows; k++)
                    {
                        matrix[i][j] += op1[i][k] * op2[k][j];
                    }
                }
            }
            return matrix;
        }
        
        
        //public Vector Current
        //{
        //    get { return matrix[rowIndex]; }
        //}
        //public void Reset()
        //{
        //    rowIndex = -1;
        //}
        public IEnumerator<Vector> GetEnumerator()
        {
            for (int i = 0; i < rows; i++)
            {
                if (matrix[i] == null)
                    break;
                yield return matrix[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

    }
}

