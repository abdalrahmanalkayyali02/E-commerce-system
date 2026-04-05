/**
 * Enum-like constant for navigation section anchors.
 */
export const NavSection = {
  Home: '#',
  Shop: '#categories',
  Collections: '#featured',
  About: '#features',
  Contact: '#newsletter',
} as const;

export type NavSection = (typeof NavSection)[keyof typeof NavSection];
