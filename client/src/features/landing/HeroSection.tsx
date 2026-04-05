import { type ReactNode } from 'react';
import { motion } from 'framer-motion';
import { ArrowRight, Sparkles } from 'lucide-react';
import heroImg from '../../assets/hero-products.png';

export function HeroSection(): ReactNode {
  return (
    <section className="hero-section" id="hero">
      {/* Animated background orbs */}
      <div className="hero-section__orbs">
        <div className="hero-section__orb hero-section__orb--1" />
        <div className="hero-section__orb hero-section__orb--2" />
        <div className="hero-section__orb hero-section__orb--3" />
      </div>

      <div className="hero-section__grid">
        {/* Left Content */}
        <div className="hero-section__content">
          <motion.div
            className="hero-section__badge"
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.6, delay: 0.2 }}
          >
            <Sparkles size={14} />
            <span>New Season Collection 2026</span>
          </motion.div>

          <motion.h1
            className="hero-section__title"
            initial={{ opacity: 0, y: 30 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.7, delay: 0.35 }}
          >
            Discover <br />
            <span className="hero-section__title-accent">Premium</span>{' '}
            Style
          </motion.h1>

          <motion.p
            className="hero-section__subtitle"
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.6, delay: 0.5 }}
          >
            Curated collections from world-class brands. Elevate your
            wardrobe with handpicked styles that define modern luxury.
          </motion.p>

          <motion.div
            className="hero-section__actions"
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.6, delay: 0.65 }}
          >
            <a href="#featured" className="btn btn--primary" id="hero-shop-now">
              Shop Now
              <ArrowRight size={18} />
            </a>
            <a href="#categories" className="btn btn--ghost" id="hero-explore">
              Explore Collections
            </a>
          </motion.div>

          <motion.div
            className="hero-section__stats"
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ duration: 0.8, delay: 0.85 }}
          >
            <div className="hero-section__stat">
              <span className="hero-section__stat-value">200+</span>
              <span className="hero-section__stat-label">Premium Brands</span>
            </div>
            <div className="hero-section__stat-divider" />
            <div className="hero-section__stat">
              <span className="hero-section__stat-value">50K+</span>
              <span className="hero-section__stat-label">Happy Customers</span>
            </div>
            <div className="hero-section__stat-divider" />
            <div className="hero-section__stat">
              <span className="hero-section__stat-value">4.9★</span>
              <span className="hero-section__stat-label">Average Rating</span>
            </div>
          </motion.div>
        </div>

        {/* Right Image */}
        <motion.div
          className="hero-section__visual"
          initial={{ opacity: 0, scale: 0.92, x: 40 }}
          animate={{ opacity: 1, scale: 1, x: 0 }}
          transition={{ duration: 0.9, delay: 0.3, ease: [0.22, 1, 0.36, 1] }}
        >
          <div className="hero-section__image-wrapper">
            <img
              src={heroImg}
              alt="Premium product showcase"
              className="hero-section__image"
            />
            <div className="hero-section__image-glow" />
          </div>
        </motion.div>
      </div>
    </section>
  );
}
