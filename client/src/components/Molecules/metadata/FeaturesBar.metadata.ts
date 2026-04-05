import { Truck, ShieldCheck, RefreshCcw, Headphones } from 'lucide-react';
import { FeatureId } from '../../../types/enum/feature-id.enum';
import type { IFeature } from '../../../types/interface/feature.interface';

export const features: readonly IFeature[] = [
  {
    id: FeatureId.Shipping,
    icon: Truck,
    title: 'Free Shipping',
    description: 'Free delivery on all orders over $50. Fast & reliable worldwide shipping.',
  },
  {
    id: FeatureId.Secure,
    icon: ShieldCheck,
    title: 'Secure Payment',
    description: 'Your payment information is encrypted and processed securely.',
  },
  {
    id: FeatureId.Returns,
    icon: RefreshCcw,
    title: 'Easy Returns',
    description: '30-day hassle-free return policy. No questions asked.',
  },
  {
    id: FeatureId.Support,
    icon: Headphones,
    title: '24/7 Support',
    description: 'Our team is here to help you anytime, day or night.',
  },
] as const;
