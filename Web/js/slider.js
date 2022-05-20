//Ofuscamos el código para evitar que si hay dos funciones con nombres iguales en dos scripts diferentes tengan el mismo ámbito
(function(){
    const sliders = [...document.querySelectorAll(".slider-body")];//Convertimos el resultado de Nodos a un array
    const arrowNext = document.querySelector('#next');
    const arrowBefore = document.querySelector('#before');
    let value

    arrowNext.addEventListener('click', ()=>changePosition(1));
    arrowBefore.addEventListener('click', ()=>changePosition(-1));

    function changePosition(change){
        const currentElement = Number(document.querySelector('.slider-body-show').dataset.id) ;

        value = currentElement;
        value += change;

        //Analizamos si es 0 para darle un comportamiento cíclico y que no se pare en el primer o último elemento
        if(value == 0 || value == sliders.length + 1){
            value = value === 0 ? sliders.length : 1;
        }

        //Eliminamos la clase slider-body-show del elemento actual y se la ponemos al siguiente para que se muestre
        sliders[currentElement-1].classList.toggle('slider-body-show');
        sliders[value-1].classList.toggle('slider-body-show');
    }
})()