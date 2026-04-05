import { ProductTag } from '../../../types/enum/product-tag.enum';
import type { IProduct } from '../../../types/interface/product.interface';
import sneakersImg from '../../../assets/product-sneakers.png';
import headphonesImg from '../../../assets/product-headphones.png';
import watchImg from '../../../assets/product-watch.png';
import bagImg from '../../../assets/product-bag.png';

export const products: readonly IProduct[] = [
  {
    id: 'prod-1',
    name: 'Air Max Pulse Elite',
    brand: 'Nike',
    price: 189.99,
    originalPrice: 249.99,
    rating: 4.8,
    reviews: 324,
    image: sneakersImg,
    tag: ProductTag.BestSeller,
  },
  {
    id: 'prod-2',
    name: 'Studio Pro Wireless',
    brand: 'Sony',
    price: 349.99,
    rating: 4.9,
    reviews: 512,
    image: headphonesImg,
    tag: ProductTag.New,
  },
  {
    id: 'prod-3',
    name: 'Chronograph Prestige',
    brand: 'Omega',
    price: 599.99,
    originalPrice: 749.99,
    rating: 4.7,
    reviews: 186,
    image: watchImg,
    tag: ProductTag.Limited,
  },
  {
    id: 'prod-4',
    name: 'Heritage Crossbody',
    brand: 'Coach',
    price: 275.0,
    rating: 4.6,
    reviews: 278,
    image: bagImg,
  },
] as const;
