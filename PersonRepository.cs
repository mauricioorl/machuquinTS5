﻿using rtoasa5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtoasa5
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection conn;
        public string statusMessage { get; set; }

        public void Init()
        {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddNewPerson(Persona person)
        {
            int result = 0;
            try
            {
                Init();
                if (person == null)
                    throw new ArgumentNullException(nameof(person), "El Dato no puede ser nulo");
                if (string.IsNullOrEmpty(person.Name))
                    throw new Exception("El nombre es requerido");

                if (person.Id == 0)
                {
                    // Insertar una nueva persona
                    result = conn.Insert(person);
                    statusMessage = string.Format("Dato agregado correctamente: {0}", person.Name);
                }
                else
                {
                    // Actualizar una persona existente
                    result = conn.Update(person);
                    statusMessage = string.Format("Se actualizo el dato correctamente: {0}", person.Name);
                }
            }
            catch (Exception ex)
            {
                statusMessage = string.Format("Error al agregar o actualizar El dato: {0}", ex.Message);
            }
        }

        public List<Persona> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {

                statusMessage = string.Format("Error al mostrar los datos", ex.Message);
            }
            return new List<Persona>();
        }

        public void DeletePerson(int Id)
        {
            try
            {
                if (Id == 0)
                    throw new ArgumentException("El ID no puede ser cero");

                conn.Delete<Persona>(Id);
                statusMessage = string.Format("Dato eliminado correctamente");
            }
            catch (Exception ex)
            {
                statusMessage = string.Format("Error al eliminar el dato: {0}", ex.Message);
            }
        }

    }
}
