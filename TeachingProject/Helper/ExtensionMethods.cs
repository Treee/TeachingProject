using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TeachingProject.Topics;

namespace TeachingProject.Helper
{   //extension methods are static so the class containing them must also be static (helper library type stuff)
    public static class ExtensionMethods
    {
        /// <summary>
        /// This method returns an IEnumerable T of a given object. 
        /// </summary>
        /// <typeparam name="T">The type of objects you want in the IEnumerable</typeparam>
        /// <param name="reader">The data reader being read from</param>
        /// <param name="projection">the function to use</param>
        /// <returns>returns an IEnumerable of type T</returns>
        public static IEnumerable<T> Select<T>(this IDataReader reader, Func<IDataReader, T> projection)
        {
            while (reader.Read())
            {//yield return is super sugar. google it.
                yield return projection(reader);//the tl;dr is it basically returns one thing at a time and only returns things when asked to do so.
            }            
        }

        /// <summary>
        /// Takes a data reader and a generic then maps the data to a list of those generics
        /// </summary>
        /// <typeparam name="T">Generic Type to convert the data to</typeparam>
        /// <param name="reader">Data reader to read from</param>
        /// <returns>Returns an IEnumerable</returns>
        public static IEnumerable<T> FromDataReader<T>(this IDataReader reader)
        {   //throw an exception if we get this far without a reader
            if (reader == null)
                throw new InvalidOperationException();
            //get the columns from the data reader (read all fields associated with the reader)
            var cols = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
            //select each row into a object that represents the data
            return reader.Select(row =>
            {   //instantiate the generic object
                var obj = Activator.CreateInstance<T>();
                //for each column
                foreach (var col in cols)
                {
                    //get the property of the object where the name matches the data reader (column from the database)
                    var colprop = obj.GetType().GetProperties().SingleOrDefault(p => p.Name == col);
                    //if colprop is not null, then set the value of the object with the given column's value
                    //if you get some sort of error here where it says it can't cast to a specific type, just use CONVERT(type, field) in the sql to cast there
                    colprop?.SetValue(obj, row[col] == DBNull.Value ? null : row[col]);
                } //return the object
                return obj;
            });
        }

        /// <summary>
        /// Takes a data reader and a generic then maps the data to a list of those generics
        /// </summary>
        /// <typeparam name="T">Generic Type to convert the data to</typeparam>
        /// <param name="reader">Data reader to read from</param>
        /// <returns>Returns an IEnumerable</returns>
        public static IEnumerable<T> FromDataTableReader<T>(this DataTableReader reader)
        {   //throw an exception if we get this far without a reader
            if (reader == null)
                throw new InvalidOperationException();
            //get the columns from the data reader (read all fields associated with the reader)
            var cols = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
            //select each row into a object that represents the data
            return reader.Select(row =>
            {   //instantiate the generic object
                var obj = Activator.CreateInstance<T>();
                //for each column
                foreach (var col in cols)
                {
                    //get the property of the object where the name matches the data reader (column from the database)
                    var colprop = obj.GetType().GetProperties().SingleOrDefault(p => p.Name == col);
                    //if colprop is not null, then set the value of the object with the given column's value
                    //if you get some sort of error here where it says it can't cast to a specific type, just use CONVERT(type, field) in the sql to cast there
                    colprop?.SetValue(obj, row[col] == DBNull.Value ? null : row[col]);
                } //return the object
                return obj;
            });
        }


        /// <summary>
        /// Map's an objects properties to another type if the properties exist in the other type
        /// </summary>
        /// <typeparam name="T">source type</typeparam>
        /// <typeparam name="TT">destination type</typeparam>
        /// <param name="fromSource">source object that is to be mapped</param>
        /// <returns>returns a mapped copy of the soruce object</returns>
        public static TT MapTo<T, TT>(this T fromSource)
        {//throw an exception if the current object is null
            if (fromSource == null)
                throw new CustomException("Source object null during mapping.");
            //get the properties to migrate into the new object
            var cols = fromSource.GetType().GetProperties().ToList();
            //create the new object
            var destinationObject = Activator.CreateInstance<TT>();
            //foreach property in the source object
            foreach (var property in cols)
            {
                //get the property of the object where the name matches the data reader (column from the database)
                var colprop = destinationObject.GetType().GetProperties().SingleOrDefault(p => p.Name == property.Name);
                //if colprop is not null, then set the value of the object with the given column's value                
                colprop?.SetValue(destinationObject, property.GetValue(fromSource));
            }
            return destinationObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="fromSource"></param>
        /// <returns></returns>
        public static IEnumerable<TT> MapTo<T, TT>(this IEnumerable<T> fromSource)
        {
            if (fromSource == null)
                throw new CustomException("Source object null during mapping.");

            var destinationList = Activator.CreateInstance<List<TT>>();
            destinationList.AddRange(fromSource.Select(obj => obj.MapTo<T, TT>()));
            return destinationList;
        }

        public static MyItem MapYourItemToMine1(this YourItem yours)
        {
            var temp = new MyItem
            {
                MyId = yours.MyId,
                MyBool = yours.MyBool,
                MyName = yours.MyName
            };
            return temp;
        }

        public static string MutateString(this string input)
        {
            var temp = string.Empty;

            temp += "BLARRRRG" + input;

            return temp;
        }
    }
}
