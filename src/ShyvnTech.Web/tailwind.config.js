/** @type {import('tailwindcss').Config} */

/**
 * ShyvnTech Tailwind CSS Configuration
 * 
 * COLOR PALETTE CONFIGURATION:
 * While Tailwind CSS provides default colors, we explicitly define colors in this config for the following reasons:
 * 
 * 1. Custom Brand Colors: The 'shyvn' palette provides our brand-specific colors that aren't in Tailwind's defaults
 * 2. Single Source of Truth: Explicit definition creates a centralized reference for our design system
 * 3. Consistency: Ensures all colors have the same shade range (50-950) for predictable design patterns
 * 4. Precision Control: Allows fine-tuning of specific color values when needed
 * 5. Custom Gradients: Powers our gradient definitions with consistent color references
 * 
 * Some colors match Tailwind defaults, but explicit definition gives us complete control
 * over our color system and makes color management more maintainable for the team.
 */
module.exports = {
    content: [
        "./Components/**/*.{razor,html,cshtml}",
        "./Pages/**/*.{razor,html,cshtml}",
        "./wwwroot/**/*.{razor,html,cshtml}"
    ],
    theme: {
        extend: {
            colors: {
                // Custom brand colors - Unique to ShyvnTech
                shyvn: {
                    50: '#f0f9ff',
                    100: '#e0f2fe',
                    200: '#bae6fd',
                    300: '#7dd3fc',
                    400: '#38bdf8',
                    500: '#0ea5e9',
                    600: '#0284c7',
                    700: '#0369a1',
                    800: '#075985',
                    900: '#0c4a6e',
                },
                // Extended color palettes - Explicitly defined for consistency and control
                // These values match Tailwind defaults but are defined here for a complete design system
                cyan: {
                    50: '#ecfeff',
                    100: '#cffafe',
                    200: '#a5f3fc',
                    300: '#67e8f9',
                    400: '#22d3ee',
                    500: '#06b6d4',
                    600: '#0891b2',
                    700: '#0e7490',
                    800: '#155e75',
                    900: '#164e63',
                    950: '#083344',
                },
                sky: {
                    50: '#f0f9ff',
                    100: '#e0f2fe',
                    200: '#bae6fd',
                    300: '#7dd3fc',
                    400: '#38bdf8',
                    500: '#0ea5e9',
                    600: '#0284c7',
                    700: '#0369a1',
                    800: '#075985',
                    900: '#0c4a6e',
                    950: '#082f49',
                },
                blue: {
                    50: '#eff6ff',
                    100: '#dbeafe',
                    200: '#bfdbfe',
                    300: '#93c5fd',
                    400: '#60a5fa',
                    500: '#3b82f6',
                    600: '#2563eb',
                    700: '#1d4ed8',
                    800: '#1e40af',
                    900: '#1e3a8a',
                    950: '#172554',
                },
                indigo: {
                    50: '#eef2ff',
                    100: '#e0e7ff',
                    200: '#c7d2fe',
                    300: '#a5b4fc',
                    400: '#818cf8',
                    500: '#6366f1',
                    600: '#4f46e5',
                    700: '#4338ca',
                    800: '#3730a3',
                    900: '#312e81',
                    950: '#1e1b4b',
                },
                violet: {
                    50: '#f5f3ff',
                    100: '#ede9fe',
                    200: '#ddd6fe',
                    300: '#c4b5fd',
                    400: '#a78bfa',
                    500: '#8b5cf6',
                    600: '#7c3aed',
                    700: '#6d28d9',
                    800: '#5b21b6',
                    900: '#4c1d95',
                    950: '#2e1065',
                },
                purple: {
                    50: '#faf5ff',
                    100: '#f3e8ff',
                    200: '#e9d5ff',
                    300: '#d8b4fe',
                    400: '#c084fc',
                    500: '#a855f7',
                    600: '#9333ea',
                    700: '#7e22ce',
                    800: '#6b21a8',
                    900: '#581c87',
                    950: '#3b0764',
                },
                fuchsia: {
                    50: '#fdf4ff',
                    100: '#fae8ff',
                    200: '#f5d0fe',
                    300: '#f0abfc',
                    400: '#e879f9',
                    500: '#d946ef',
                    600: '#c026d3',
                    700: '#a21caf',
                    800: '#86198f',
                    900: '#701a75',
                    950: '#4a044e',
                },
            },
            fontFamily: {
                sans: ['Inter', 'ui-sans-serif', 'system-ui', 'sans-serif'],
                'poppins': ['Poppins', '-apple-system', 'BlinkMacSystemFont', 'Segoe UI', 'Roboto', 'sans-serif']
            },
            backgroundImage: {
                // Custom gradients using our defined color system
                // These create consistent gradient patterns across the application
                // Each uses a lighter shade (300/400) to a darker shade (600/700) of the same color
                'hero-gradient': 'linear-gradient(135deg, #f8fafc, #e0e7ff)',
                'cyan-gradient': 'linear-gradient(135deg, #67e8f9, #0891b2)',
                'sky-gradient': 'linear-gradient(135deg, #7dd3fc, #0284c7)',
                'blue-gradient': 'linear-gradient(135deg, #60a5fa, #1d4ed8)',
                'indigo-gradient': 'linear-gradient(135deg, #818cf8, #4338ca)',
                'violet-gradient': 'linear-gradient(135deg, #a78bfa, #6d28d9)',
                'purple-gradient': 'linear-gradient(135deg, #c084fc, #7e22ce)',
                'fuchsia-gradient': 'linear-gradient(135deg, #e879f9, #a21caf)',
            },
        },
    },
    plugins: [],
}
