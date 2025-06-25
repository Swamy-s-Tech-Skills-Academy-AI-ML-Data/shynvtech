// Magazine JavaScript Helper Functions

/**
 * Downloads a file from the given URL with the specified filename
 * @param {string} url - The URL of the file to download
 * @param {string} filename - The desired filename for the download
 */
window.downloadFile = function(url, filename) {
    try {
        // Create a temporary anchor element
        const link = document.createElement('a');
        link.href = url;
        link.download = filename || 'download';
        link.target = '_blank';
        
        // Add to DOM, trigger click, then remove
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        
        // Show success feedback
        showNotification('Download started successfully!', 'success');
    } catch (error) {
        console.error('Download failed:', error);
        showNotification('Download failed. Please try again.', 'error');
    }
};

/**
 * Opens a URL in a new tab/window
 * @param {string} url - The URL to open
 * @param {string} target - The target window (default: '_blank')
 */
window.openInNewTab = function(url, target = '_blank') {
    try {
        window.open(url, target, 'noopener,noreferrer');
    } catch (error) {
        console.error('Failed to open URL:', error);
        showNotification('Failed to open link. Please try again.', 'error');
    }
};

/**
 * Shows a notification to the user
 * @param {string} message - The notification message
 * @param {string} type - The notification type ('success', 'error', 'info')
 */
function showNotification(message, type = 'info') {
    // Create notification element
    const notification = document.createElement('div');
    notification.className = `fixed top-4 right-4 z-50 px-6 py-4 rounded-lg shadow-lg transform transition-all duration-300 translate-x-full`;
    
    // Set notification style based on type
    switch (type) {
        case 'success':
            notification.className += ' bg-green-500 text-white';
            break;
        case 'error':
            notification.className += ' bg-red-500 text-white';
            break;
        default:
            notification.className += ' bg-blue-500 text-white';
    }
    
    // Add icon and message
    const icon = type === 'success' ? '✓' : type === 'error' ? '✗' : 'ℹ';
    notification.innerHTML = `
        <div class="flex items-center">
            <span class="text-lg mr-2">${icon}</span>
            <span>${message}</span>
            <button onclick="this.parentElement.parentElement.remove()" class="ml-4 text-white hover:text-gray-200">
                ✕
            </button>
        </div>
    `;
    
    // Add to DOM
    document.body.appendChild(notification);
    
    // Animate in
    setTimeout(() => {
        notification.classList.remove('translate-x-full');
    }, 100);
    
    // Auto remove after 5 seconds
    setTimeout(() => {
        if (notification.parentNode) {
            notification.classList.add('translate-x-full');
            setTimeout(() => {
                if (notification.parentNode) {
                    notification.remove();
                }
            }, 300);
        }
    }, 5000);
}

/**
 * Checks if a PDF URL is valid and accessible
 * @param {string} pdfUrl - The PDF URL to check
 * @returns {Promise<boolean>} - True if accessible, false otherwise
 */
window.checkPDFAvailability = async function(pdfUrl) {
    try {
        const response = await fetch(pdfUrl, { method: 'HEAD' });
        return response.ok;
    } catch (error) {
        console.error('PDF availability check failed:', error);
        return false;
    }
};

/**
 * Preloads an image for better user experience
 * @param {string} imageUrl - The image URL to preload
 */
window.preloadImage = function(imageUrl) {
    if (imageUrl) {
        const img = new Image();
        img.src = imageUrl;
    }
};

/**
 * Handles magazine card animations and interactions
 */
window.initializeMagazineAnimations = function() {
    // Add intersection observer for scroll animations
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('animate-fade-in');
            }
        });
    }, {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    });

    // Observe all magazine cards
    document.querySelectorAll('[data-magazine-card]').forEach(card => {
        observer.observe(card);
    });
};

/**
 * Smooth scroll to element
 * @param {string} elementId - The ID of the element to scroll to
 */
window.smoothScrollTo = function(elementId) {
    const element = document.getElementById(elementId);
    if (element) {
        element.scrollIntoView({
            behavior: 'smooth',
            block: 'start'
        });
    }
};

// Initialize when DOM is ready
document.addEventListener('DOMContentLoaded', function() {
    // Initialize magazine animations
    if (window.initializeMagazineAnimations) {
        window.initializeMagazineAnimations();
    }
    
    // Add custom CSS for animations
    const style = document.createElement('style');
    style.textContent = `
        .animate-fade-in {
            animation: fadeInUp 0.6s ease-out forwards;
        }
        
        @keyframes fadeInUp {
            from {
                opacity: 0;
                transform: translateY(30px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }
        
        .animate-reverse {
            animation-direction: reverse;
        }
        
        .magazine-card {
            transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
        }
        
        .magazine-card:hover {
            transform: translateY(-8px) scale(1.02);
        }
    `;
    document.head.appendChild(style);
});

// Export functions for testing
if (typeof module !== 'undefined' && module.exports) {
    module.exports = {
        downloadFile: window.downloadFile,
        openInNewTab: window.openInNewTab,
        checkPDFAvailability: window.checkPDFAvailability,
        preloadImage: window.preloadImage
    };
} 