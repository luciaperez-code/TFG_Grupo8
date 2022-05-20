(function(){
    const listElements = document.querySelectorAll('.menu-item-show');
    const list = document.querySelector('.menu-links');
    const menu = document.querySelector('.menu-hamburger');

    const addClick = ()=>{
        listElements.forEach(element =>{
            element.addEventListener('click',  ()=>{

                let subMenu = element.children[1];
                let height = 0;

                element.classList.toggle('menu-item-active');

                //Si la altura del elemeto es 0 entra al if
                if(subMenu.clientHeight === 0){
                    //ScrollHeight devuelve la altura mínima para que exista el elemento
                    height = subMenu.scrollHeight;
                }

                subMenu.style.height = `${height}px`;

            })
        })
    }

    const deleteStyleHeight = ()=>{
        listElements.forEach(element =>{

            if(element.children[1].getAttribute('style')){
                element.children[1].removeAttribute('style');
                element.classList.remove('menu-item-active');
            }

        });
    }

    window.addEventListener('resize', ()=>{
        if(window.innerWidth > 800){
            deleteStyleHeight();

            if(list.classList.contains('menu-links-show')){
                list.classList.remove('menu-links-show')
            }
        }else{
            addClick();
        }
    })

    if(window.innerWidth <= 800){
        addClick();
    }

    menu.addEventListener('click', ()=> list.classList.toggle('menu-links-show'));

})();

