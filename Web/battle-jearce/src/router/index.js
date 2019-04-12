import Vue from 'vue'
import Router from 'vue-router'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import Classement from '@/components/Classement'
import Header from '@/components/Header'
import Accueil from '@/components/Accueil'
import BootstrapVue from 'bootstrap-vue'
import Navbar from '@/components/Navbar'
import Profil from '@/components/Profil'
import Boutique from '@/components/Boutique'
import Presentation from '@/components/Presentation'
import Inscription from '@/components/Inscription'
import Connexion from '@/components/Connexion'
import Reglement from '@/components/Reglement'
import Contact from '@/components/Contact'
import Validation from '@/components/Validation'
import Formation from '@/components/Formation'

import VueAxios from 'vue-axios';
import axios from 'axios';
Vue.use(VueAxios, axios);
Vue.use(Router)
Vue.use(BootstrapVue)

export default new Router({
  routes: [
    {
      path: '/',
      redirect:'/Battle-Jearce/Accueil'
    },
    {
      path: '/Battle-Jearce',
      name: 'Battle-Jearce',
      component: Accueil,
      children: [
        {
          path: 'Accueil',
          components: {
            default: Header,
            body: Presentation,
            navBar: Navbar
          }
        },
        {
          path: 'Reglement',
          components: {
            default: Header,
            body: Reglement,
            navBar: Navbar
          }
        },
        {
          path: 'Contact',
          components: {
            default: Header,
            body: Contact,
            navBar: Navbar
          }
        },
        {
          path: 'Inscription',
          components: {
            default: Header,
            body: Inscription,
            navBar: Navbar
          }
        },
        {
          path: 'Connexion',
          components: {
            default: Header,
            body: Connexion,
            navBar: Navbar
          }
        },
        {
          path: 'Classement',
          components: {
            default: Header,
            body: Classement,
            navBar: Navbar
          }
        },
        {
          path: 'Profil',
          components: {
            default: Header,
            body: Profil,
            navBar: Navbar
          }
        },
        {
          path: 'Boutique',
          components: {
            default: Header,
            body: Boutique,
            navBar: Navbar
          }
        },
        {
          path: 'Formation',
          components: {
            default: Header,
            body: Boutique,
            navBar: Navbar
          }
        }
      ]
    },
    {
      path: '/Validation',
      name: 'validation',
      component: Validation,
    }
  ]
})
