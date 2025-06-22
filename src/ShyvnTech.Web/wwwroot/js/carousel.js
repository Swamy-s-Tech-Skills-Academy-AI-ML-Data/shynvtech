function initCarousel() {
    const track = document.getElementById('carousel-track');
    const prevBtn = document.getElementById('prev-btn');
    const nextBtn = document.getElementById('next-btn');
    const indicators = document.querySelectorAll('.flex.justify-center.mt-6 button');

    let currentIndex = 0;
    const slideWidth = track.children[0].offsetWidth;
    const slideCount = track.children.length;

    function updateCarousel() {
        // Update track position
        track.style.transform = `translateX(${-currentIndex * slideWidth}px)`;

        // Update indicators
        indicators.forEach((dot, index) => {
            dot.classList.toggle('bg-blue-600', index === currentIndex);
            dot.classList.toggle('bg-gray-300', index !== currentIndex);
        });
    }

    // Event listeners
    prevBtn.addEventListener('click', () => {
        currentIndex = (currentIndex - 1 + slideCount) % slideCount;
        updateCarousel();
    });

    nextBtn.addEventListener('click', () => {
        currentIndex = (currentIndex + 1) % slideCount;
        updateCarousel();
    });

    // Add click listeners to indicators
    indicators.forEach((dot, index) => {
        dot.addEventListener('click', () => {
            currentIndex = index;
            updateCarousel();
        });
    });

    // Auto slide every 5 seconds
    setInterval(() => {
        currentIndex = (currentIndex + 1) % slideCount;
        updateCarousel();
    }, 5000);

    // Handle window resize
    window.addEventListener('resize', () => {
        const newSlideWidth = track.children[0].offsetWidth;
        track.style.transform = `translateX(${-currentIndex * newSlideWidth}px)`;
    });
}
