
<template>
    <div><h1>Formulario para crear un pais</h1></div>
    <div class="d-flex justify-content-center align-items-center vh-100">
      <div class="card p-4 shadow" style="max-width: 400px; width: 100%">
        <h3 class="text-center">
          Formulario de creacion de paises
        </h3>
        <form @submit.prevent="guardarPais">
          <div class="form-group">
            <label for="nombre">Nombre:</label>
            <input v-model="datosFormulario.nombre"
                   type="text"
                   id="name"
                   class="form-control"
                   required />
          </div>
          <div class="form-group">
            <label for="continente">Continente:</label>
            <select v-model="datosFormulario.continente"
                    id="continente"
                    required
                    class="form-control">
              <option value="" disabled>Seleccione un continente </option>
              <option>Africa</option>
              <option>Asia</option>
              <option>Europa</option>
              <option>America</option>
              <option>Oceania</option>
              <option>Antartida</option>
            </select>
          </div>
          <div class="form-group">
            <label for="idioma">Idioma:</label>
            <input v-model="datosFormulario.idioma"
                   type="text"
                   id="idioma"
                   class="form-control"
                   required />
          </div>
          <div>
            <button type="submit" class="btn btn-success btn-block">
              Guardar
            </button>
          </div>
        </form>
        <div v-if="mostrarMensaje" class="form-control mt-3" readonly>
          {{ mensaje }}
        </div>
      </div>
    </div>
</template>

<script>
    import axios from "axios";
    export default {
        data() {
            return {
              datosFormulario: { nombre: "", continente: "", idioma: "" },
              mensaje: "",
              mostrarMensaje: false,
            };
        },
        methods: {
            guardarPais() {
                this.mostrarMensaje = false;
                this.mensaje = "";
                axios.post("https://localhost:7093/api/Paises", {
                    nombre: this.datosFormulario.nombre,
                    continente: this.datosFormulario.continente,
                    idioma: this.datosFormulario.idioma,
                }).then((response) => {
                  console.log(response);
                  if (response != null && response.data) {
                    this.mensaje = "Pais guardado exitosamente!";
                    this.mostrarMensaje = true;
                  } else {
                    this.mensaje = "No se pudo agregar pais. Intente mas tarde.";
                    this.mostrarMensaje = true;
                  }
                }).catch((error) => {
                  this.mensaje = "No se pudo agregar pais. Intente mas tarde.";
                  this.mostrarMensaje = true;
                  console.error(error);
                });
            },
        },
    };
</script>

<style>
</style>