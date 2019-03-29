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

Vue.use(Router)
Vue.use(BootstrapVue)

export default new Router({
  routes: [
    {
      path: '/',
      redirect:'/Battle-Jearce/Classement'
    },
    {
      path: '/Battle-Jearce',
      name: 'Battle-Jearce',
      component: Accueil,
      children: [
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
        }
      ]
    }
  ]
})
