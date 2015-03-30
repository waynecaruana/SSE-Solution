﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace Common
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class SecureShoppingCartDBEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new SecureShoppingCartDBEntities object using the connection string found in the 'SecureShoppingCartDBEntities' section of the application configuration file.
        /// </summary>
        public SecureShoppingCartDBEntities() : base("name=SecureShoppingCartDBEntities", "SecureShoppingCartDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new SecureShoppingCartDBEntities object.
        /// </summary>
        public SecureShoppingCartDBEntities(string connectionString) : base(connectionString, "SecureShoppingCartDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new SecureShoppingCartDBEntities object.
        /// </summary>
        public SecureShoppingCartDBEntities(EntityConnection connection) : base(connection, "SecureShoppingCartDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Genre> Genres
        {
            get
            {
                if ((_Genres == null))
                {
                    _Genres = base.CreateObjectSet<Genre>("Genres");
                }
                return _Genres;
            }
        }
        private ObjectSet<Genre> _Genres;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Genres EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToGenres(Genre genre)
        {
            base.AddObject("Genres", genre);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="SecureShoppingCartDBModel", Name="Genre")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Genre : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Genre object.
        /// </summary>
        /// <param name="genreID">Initial value of the GenreID property.</param>
        public static Genre CreateGenre(global::System.Int32 genreID)
        {
            Genre genre = new Genre();
            genre.GenreID = genreID;
            return genre;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 GenreID
        {
            get
            {
                return _GenreID;
            }
            set
            {
                if (_GenreID != value)
                {
                    OnGenreIDChanging(value);
                    ReportPropertyChanging("GenreID");
                    _GenreID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("GenreID");
                    OnGenreIDChanged();
                }
            }
        }
        private global::System.Int32 _GenreID;
        partial void OnGenreIDChanging(global::System.Int32 value);
        partial void OnGenreIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description;
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();

        #endregion

    
    }

    #endregion

    
}
