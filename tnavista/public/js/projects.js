document.addEventListener('DOMContentLoaded', function() {
    const filterBtns = document.querySelectorAll('.model-filter-btn-ultimate');
    const cards = document.querySelectorAll('.project__card-ultimate');

    filterBtns.forEach(btn => {
        btn.addEventListener('click', function() {
            filterBtns.forEach(b => b.classList.remove('active'));
            this.classList.add('active');
            const model = this.getAttribute('data-model');
            cards.forEach(card => {
                const cardModel = card.getAttribute('data-category');
                if (model === 'all' || cardModel === model) {
                    card.style.display = '';
                } else {
                    card.style.display = 'none';
                }
            });
        });
    });
}); 