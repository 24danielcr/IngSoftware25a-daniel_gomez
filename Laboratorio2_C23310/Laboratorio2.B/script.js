var lista = document.getElementById('lista');
function agregar() {
    let elemento = document.createElement('li');
    elemento.innerText = "Elemento" + (lista.children.length + 1);
    lista.append(elemento);
}

function borrar() {
    let tamLista = lista.children.length;
    (tamLista == 0) ? console.log("No se pueden eliminar mas elementos") : lista.children[tamLista - 1].remove();
}

function cambiarFondo() {
    let colorFondo = document.body.style.backgroundColor;
    document.body.style.backgroundColor = (colorFondo == "green") ? "white" : "green";
}