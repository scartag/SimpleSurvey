import Vue from 'vue';
import VueRouter from 'vue-router';
import { routes } from './routes';

Vue.use(VueRouter);

let router = new VueRouter({
  mode: 'history',
  routes
});

router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const loggedIn = window.localStorage.getItem('user');

  if (!loggedIn) {
    window.location.href = "/home/login";
  }

  next();
})

export default router;
