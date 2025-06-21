# ShyvnTech Web Styling Guide

This document outlines the styling approach for the ShyvnTech Web application, focusing on our Tailwind CSS implementation and custom styling strategies.

## Table of Contents

- [Tailwind Configuration](#tailwind-configuration)
- [Custom Component Classes](#custom-component-classes)
- [Theme Usage](#theme-usage)
- [Styling Best Practices](#styling-best-practices)
- [Project Structure](#project-structure)

## Tailwind Configuration

Our project uses Tailwind CSS v3.4.17, configured in `tailwind.config.js`. The configuration extends Tailwind's default theme with:

- Custom ShyvnTech brand colors
- Custom font families
- Gradient definitions

### Key Configuration Files

- `tailwind.config.js`: Core configuration with theme extensions
- `Styles/input.css`: Source CSS with custom components and utilities
- `wwwroot/css/tailwind.css`: Generated output (do not edit directly)

## Custom Component Classes

We use custom component classes in the `@layer components` section of `input.css` for:

1. **Consistency**: Ensuring UI elements look the same throughout the application
2. **Reusability**: Creating semantic components that can be easily applied
3. **Maintainability**: Making site-wide style updates in one place

### When to Use Custom Classes vs. Utility Classes

- **Use custom component classes** for repeated patterns like buttons, navigation links, or cards
- **Use Tailwind utility classes directly** for one-off styling or minor adjustments

## Theme Usage

### Using `theme()` Function

In our custom CSS, we use the `theme()` function to access values from our Tailwind config:

```css
.text-shyvn-700 {
  color: theme("colors.shyvn.700");
}
```

This approach:

- Ensures consistency with our defined theme
- Centralizes color management in `tailwind.config.js`
- Makes updates easier (change once, apply everywhere)

### Custom Colors

Our brand color palette is defined as `shyvn-{50-900}` following Tailwind's shade numbering convention. These colors can be used either:

1. Directly in HTML with Tailwind classes:

   ```html
   <div class="text-shyvn-700 bg-shyvn-100">Content</div>
   ```

2. In custom CSS components using `theme()`:

   ```css
   .my-component {
     color: theme("colors.shyvn.700");
   }
   ```

## Styling Best Practices

1. **Prefer Tailwind utilities** when possible for direct HTML styling
2. **Create custom components** (in `input.css`) for repeated patterns
3. **Use `@layer` correctly**:
   - `@layer components` for reusable UI components
   - `@layer utilities` for custom utilities that should have the same precedence as Tailwind utilities
4. **Document CSS** with comments explaining the purpose and any equivalent Tailwind classes

## Project Structure

- **`/Styles/input.css`**: Source CSS with all custom components and utilities
- **`/wwwroot/css/tailwind.css`**: Generated CSS (do not edit)
- **`/wwwroot/css/app.css`**: Blazor-specific styles that can't be replaced with Tailwind
- **`/Components/**/\*.razor`\*\*: Blazor components using Tailwind classes

## Build Commands

- Development (watch mode): `npm run build-css`
- Production (minified): `npm run build-css-prod`
