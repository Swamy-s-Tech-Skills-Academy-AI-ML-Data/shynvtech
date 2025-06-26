// Carousel functionality for ShyvnTech Web
let currentSlide = 0;
const totalSlides = 3;
let autoRotateInterval;

// Initialize carousel
function initCarousel() {
    console.log('Initializing carousel...');

    const track = document.getElementById('carousel-track');
    const prevBtn = document.getElementById('prev-btn');
    const nextBtn = document.getElementById('next-btn');
    const indicators = document.querySelectorAll('.carousel-indicator');

    if (!track || !prevBtn || !nextBtn) {
        console.log('Carousel elements not found, retrying...');
        setTimeout(initCarousel, 100);
        return;
    }

    // Set up initial state
    updateCarousel();

    // Add event listeners
    prevBtn.addEventListener('click', prevSlide);
    nextBtn.addEventListener('click', nextSlide);

    // Add indicator click events
    indicators.forEach((indicator, index) => {
        indicator.addEventListener('click', () => goToSlide(index));
    });

    // Add touch/swipe support
    let startX = 0;
    let endX = 0;

    track.addEventListener('touchstart', (e) => {
        startX = e.touches[0].clientX;
    });

    track.addEventListener('touchmove', (e) => {
        e.preventDefault();
    });

    track.addEventListener('touchend', (e) => {
        endX = e.changedTouches[0].clientX;
        handleSwipe();
    });

    // Mouse drag support
    let isDragging = false;
    let startMouseX = 0;
    let endMouseX = 0;

    track.addEventListener('mousedown', (e) => {
        isDragging = true;
        startMouseX = e.clientX;
        track.style.cursor = 'grabbing';
    });

    track.addEventListener('mousemove', (e) => {
        if (!isDragging) return;
        e.preventDefault();
        endMouseX = e.clientX;
    });

    track.addEventListener('mouseup', (e) => {
        if (!isDragging) return;
        isDragging = false;
        track.style.cursor = 'grab';
        endMouseX = e.clientX;
        handleSwipe();
    });

    track.addEventListener('mouseleave', () => {
        isDragging = false;
        track.style.cursor = 'grab';
    });

    // Auto-rotation
    startAutoRotate();

    // Pause auto-rotation on hover
    track.addEventListener('mouseenter', stopAutoRotate);
    track.addEventListener('mouseleave', startAutoRotate);

    console.log('Carousel initialized successfully');
}

function handleSwipe() {
    const swipeThreshold = 50;
    const diff = startX || startMouseX - (endX || endMouseX);

    if (Math.abs(diff) > swipeThreshold) {
        if (diff > 0) {
            nextSlide();
        } else {
            prevSlide();
        }
    }

    startX = 0;
    endX = 0;
    startMouseX = 0;
    endMouseX = 0;
}

function updateCarousel() {
    const track = document.getElementById('carousel-track');
    const indicators = document.querySelectorAll('.carousel-indicator');

    if (!track) return;

    // Move track to show current slide (each slide takes 33.333% of the track width)
    const translateX = -(currentSlide * 33.333);
    track.style.transform = `translateX(${translateX}%)`;
    track.style.transition = 'transform 0.5s cubic-bezier(0.4, 0, 0.2, 1)';

    // Update indicators
    indicators.forEach((indicator, index) => {
        if (index === currentSlide) {
            indicator.classList.remove('carousel-indicator-inactive');
            indicator.classList.add('carousel-indicator-active');
        } else {
            indicator.classList.remove('carousel-indicator-active');
            indicator.classList.add('carousel-indicator-inactive');
        }
    });

    console.log(`Showing slide ${currentSlide + 1} of ${totalSlides}`);
}

function nextSlide() {
    currentSlide = (currentSlide + 1) % totalSlides;
    updateCarousel();
    resetAutoRotate();
}

function prevSlide() {
    currentSlide = (currentSlide - 1 + totalSlides) % totalSlides;
    updateCarousel();
    resetAutoRotate();
}

function goToSlide(slideIndex) {
    currentSlide = slideIndex;
    updateCarousel();
    resetAutoRotate();
}

function startAutoRotate() {
    stopAutoRotate();
    autoRotateInterval = setInterval(() => {
        nextSlide();
    }, 5000); // Rotate every 5 seconds
}

function stopAutoRotate() {
    if (autoRotateInterval) {
        clearInterval(autoRotateInterval);
        autoRotateInterval = null;
    }
}

function resetAutoRotate() {
    stopAutoRotate();
    startAutoRotate();
}

// Initialize when DOM is ready
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initCarousel);
} else {
    initCarousel();
}
