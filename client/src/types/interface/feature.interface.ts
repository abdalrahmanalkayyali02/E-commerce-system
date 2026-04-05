import type { LucideIcon } from 'lucide-react';
import type { FeatureId } from '../enum/feature-id.enum';

/**
 * Represents a trust-signal / feature item in the features bar.
 */
export interface IFeature {
  readonly id: FeatureId;
  readonly icon: LucideIcon;
  readonly title: string;
  readonly description: string;
}
