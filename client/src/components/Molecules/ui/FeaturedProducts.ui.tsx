import type { ReactNode } from 'react';
import { useState } from 'react';
import { motion } from 'framer-motion';
import { Heart, ShoppingBag, Star, Eye } from 'lucide-react';
import { products } from '../metadata/FeaturedProducts.metadata';
import type { IProduct } from '../../../types/interface/product.interface';

const containerVariants = {
  hidden: {},
  visible: { transition: { staggerChildren: 0.12 } },
};

const cardVariants = {
  hidden: { opacity: 0, y: 50 },
  visible: {
    opacity: 1,
    y: 0,
    transition: { duration: 0.6, ease: [0.22, 1, 0.36, 1] as const },
  },
};

interface ProductCardProps {
  readonly product: IProduct;
}

function ProductCard({ product }: ProductCardProps): ReactNode {
  const [liked, setLiked] = useState<boolean>(false);

  const discount: number | null = product.originalPrice
    ? Math.round(((product.originalPrice - product.price) / product.originalPrice) * 100)
    : null;

  const tagCssClass: string = product.tag
    ? `product-card__tag product-card__tag--${product.tag.toLowerCase().replace(' ', '-')}`
    : '';

  return (
    <motion.div
      className="product-card"
      id={product.id}
      variants={cardVariants}
      whileHover={{ y: -6 }}
    >
      <div className="product-card__image-wrapper">
        <img
          src={product.image}
          alt={product.name}
          className="product-card__image"
        />

        {product.tag && (
          <span className={tagCssClass}>
            {product.tag}
          </span>
        )}
        {discount !== null && (
          <span className="product-card__discount">-{discount}%</span>
        )}

        <div className="product-card__overlay">
          <button
            className={`product-card__action ${liked ? 'product-card__action--liked' : ''}`}
            aria-label="Add to wishlist"
            onClick={() => setLiked(!liked)}
          >
            <Heart size={18} fill={liked ? 'currentColor' : 'none'} />
          </button>
          <button className="product-card__action" aria-label="Quick view">
            <Eye size={18} />
          </button>
          <button className="product-card__action product-card__action--cart" aria-label="Add to cart">
            <ShoppingBag size={18} />
          </button>
        </div>
      </div>

      <div className="product-card__info">
        <span className="product-card__brand">{product.brand}</span>
        <h3 className="product-card__name">{product.name}</h3>
        <div className="product-card__rating">
          <Star size={14} fill="currentColor" className="product-card__star" />
          <span>{product.rating}</span>
          <span className="product-card__reviews">({product.reviews})</span>
        </div>
        <div className="product-card__pricing">
          <span className="product-card__price">${product.price.toFixed(2)}</span>
          {product.originalPrice !== undefined && (
            <span className="product-card__original-price">
              ${product.originalPrice.toFixed(2)}
            </span>
          )}
        </div>
      </div>
    </motion.div>
  );
}

export function FeaturedProducts(): ReactNode {
  return (
    <section className="featured" id="featured">
      <div className="featured__header">
        <motion.span
          className="section-tag"
          initial={{ opacity: 0 }}
          whileInView={{ opacity: 1 }}
          viewport={{ once: true }}
        >
          Top Picks for You
        </motion.span>
        <motion.h2
          className="section-title"
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ delay: 0.1 }}
        >
          Featured Products
        </motion.h2>
        <motion.p
          className="section-subtitle"
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ delay: 0.2 }}
        >
          Handpicked products that our customers love the most
        </motion.p>
      </div>

      <motion.div
        className="featured__grid"
        variants={containerVariants}
        initial="hidden"
        whileInView="visible"
        viewport={{ once: true, amount: 0.15 }}
      >
        {products.map((product: IProduct) => (
          <ProductCard key={product.id} product={product} />
        ))}
      </motion.div>

      <motion.div
        className="featured__cta"
        initial={{ opacity: 0, y: 20 }}
        whileInView={{ opacity: 1, y: 0 }}
        viewport={{ once: true }}
      >
        <a href="#" className="btn btn--outline" id="view-all-products">
          View All Products
        </a>
      </motion.div>
    </section>
  );
}
