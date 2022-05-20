var ham;
var enlaces; 
var barras;
var desplegado;
var nombre;
var errNombre;
var email;
var errEmail;
var ubicacion;
var provincias;
var paises;
var check;
var marcado;
var aceptar;
var borrar;

window.addEventListener('load', () => {
    inicializarVariables();

    aceptar.addEventListener('click', comprobarFormulario());
    
    ubicacion.addEventListener("change", selectUbi);

    ham.addEventListener('click', ()=>{

        if (!desplegado) {
            enlaces.className += ' activado';
            barras.forEach(child => {child.className += ' animado'});
            desplegado= true;
        }else{
            enlaces.classList.remove('activado');
            barras.forEach(child => {child.classList.remove('animado')});
            desplegado = false;
        }
        
    });
})


//-----------Funciones------------

function comprobarFormulario(){
    if(nombre.value != 0){
        if(marcado){
            alert('Registro realizado con éxito')
    
        }else{
            alert('Por favor, acepta las condiciones de uso');
        }
    }
    
}

function selectUbi(){
    var ubi = document.getElementById('ubicacion').value;
    if(ubi == 'espana'){
        provincias.style.display = "inline";
        paises.style.display = "none";
    }else if(ubi == 'sudamerica'){
        provincias.style.display = "none";
        paises.style.display = "inline";
    }else{
        provincias.style.display = "none";
        paises.style.display = "none";
    }
}

//----------------INICIALIZAR VARIABLES-----------------

function inicializarVariables(){
    ham = document.getElementById('ham');
    enlaces = document.getElementById('enlaces-menu'); 
    barras = document.querySelectorAll('.ham span');
    desplegado = false;
    nombre = document.getElementById('nombre');
    errNombre = document.getElementById('errNombre');
    email = document.getElementById('email');
    errEmail = document.getElementById('errEmail');
    ubicacion = document.getElementById('ubicacion');
    provincias = document.getElementById('provincia');
    paises = document.getElementById('paises');
    check = document.getElementById('checkbox');
    marcado = check.checked;
    aceptar = document.getElementById('aceptar');
    borrar = document.getElementById('borrar');
}