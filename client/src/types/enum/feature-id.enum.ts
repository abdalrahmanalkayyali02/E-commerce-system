/**
 * Enum-like constant for feature/trust-signal identifiers.
 */
export const FeatureId = {
  Shipping: 'feature-shipping',
  Secure: 'feature-secure',
  Returns: 'feature-returns',
  Support: 'feature-support',
} as const;

export type FeatureId = (typeof FeatureId)[keyof typeof FeatureId];
