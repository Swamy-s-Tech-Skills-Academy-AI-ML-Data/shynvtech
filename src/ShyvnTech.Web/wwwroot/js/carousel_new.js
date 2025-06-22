/**
 * Simple Carousel Implementation
 * This script handles the carousel functionality for the ShyvnTech website
 */

// Function to initialize the carousel - made globally available
window.initCarousel = function () {
    console.log("Initializing ShyvnTech carousel...");

    // Wait for DOM to be fully ready
    setTimeout(() => {
        // DOM Elements
        const track = document.getElementById('carousel-track');
        const prevBtn = document.getElementById('prev-btn');
        const nextBtn = document.getElementById('next-btn');
        const indicators = document.querySelectorAll('.flex.justify-center.mt-6 button');

        // Verify elements exist
        if (!track) {
            console.error('Carousel track element not found');
            return;
        }

        if (!prevBtn || !nextBtn) {
            console.error('Carousel navigation buttons not found');
            return;
        }

        if (!indicators || indicators.length === 0) {
            console.error('Carousel indicators not found');
            return;
        }

        console.log(`Found carousel with ${track.children.length} items and ${indicators.length} indicators`);

        // State variables
        let currentIndex = 0;
        let autoSlideInterval = null;
        const slideCount = track.children.length;

        // Force cards to be full width for one-at-a-time viewing
        Array.from(track.children).forEach(child => {
            child.style.width = '100%';
            child.style.flexShrink = '0';
            child.style.flexGrow = '0';
        });

        // Get the width of the carousel container
        function getContainerWidth() {
            return track.parentElement.offsetWidth;
        }

        // Update the carousel position and indicators
        function updateCarousel() {
            const width = getContainerWidth();
            console.log(`Updating carousel to index ${currentIndex} (width: ${width}px)`);

            // Move the track
            track.style.transition = 'transform 0.5s ease-in-out';
            track.style.transform = `translateX(-${currentIndex * width}px)`;

            // Update indicators
            indicators.forEach((dot, index) => {
                if (index === currentIndex) {
                    dot.classList.add('bg-black');
                    dot.classList.remove('bg-gray-400');
                } else {
                    dot.classList.remove('bg-black');
                    dot.classList.add('bg-gray-400');
                }
            });
        }

        // Reset and start auto-slide timer
        function resetAutoSlide() {
            if (autoSlideInterval) {
                clearInterval(autoSlideInterval);
            }

            autoSlideInterval = setInterval(() => {
                currentIndex = (currentIndex + 1) % slideCount;
                updateCarousel();
            }, 5000);
        }

        // Navigation button event listeners
        if (prevBtn) {
            prevBtn.addEventListener('click', (e) => {
                e.preventDefault();
                console.log('Previous button clicked');
                currentIndex = (currentIndex - 1 + slideCount) % slideCount;
                updateCarousel();
                resetAutoSlide();
            });
        }

        if (nextBtn) {
            nextBtn.addEventListener('click', (e) => {
                e.preventDefault();
                console.log('Next button clicked');
                currentIndex = (currentIndex + 1) % slideCount;
                updateCarousel();
                resetAutoSlide();
            });
        }

        // Indicator dots event listeners
        indicators.forEach((dot, index) => {
            dot.addEventListener('click', (e) => {
                e.preventDefault();
                console.log(`Indicator ${index} clicked`);
                currentIndex = index;
                updateCarousel();
                resetAutoSlide();
            });
        });

        // Touch swipe functionality
        let touchStartX = 0;

        track.addEventListener('touchstart', (e) => {
            touchStartX = e.changedTouches[0].screenX;
        }, { passive: true });

        track.addEventListener('touchend', (e) => {
            const touchEndX = e.changedTouches[0].screenX;
            const diff = touchStartX - touchEndX;
            const threshold = 50;

            if (Math.abs(diff) > threshold) {
                if (diff > 0) {
                    // Swipe left -> next
                    currentIndex = (currentIndex + 1) % slideCount;
                } else {
                    // Swipe right -> previous
                    currentIndex = (currentIndex - 1 + slideCount) % slideCount;
                }
                updateCarousel();
                resetAutoSlide();
            }
        }, { passive: true });

        // Handle window resize
        window.addEventListener('resize', () => {
            // Temporarily disable transition for resize
            track.style.transition = 'none';
            updateCarousel();

            // Re-enable transition after a moment
            setTimeout(() => {
                track.style.transition = 'transform 0.5s ease-in-out';
            }, 50);
        });

        // Initialize carousel
        updateCarousel();
        resetAutoSlide();
        console.log('ShyvnTech carousel initialized successfully');
    }, 100); // Small delay to ensure DOM is ready
};

// Auto-initialize when loaded as a script
if (document.readyState === 'complete' || document.readyState === 'interactive') {
    setTimeout(initCarousel, 1);
} else {
    document.addEventListener('DOMContentLoaded', initCarousel);
}

// Export for ES modules
export { initCarousel };
