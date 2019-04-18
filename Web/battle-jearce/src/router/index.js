import Vue from 'vue'
import Router from 'vue-router'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import Classement from '@/components/BattleJearceBody/Classement'
import Header from '@/components/ElementCommun/Header'
import Accueil from '@/components/Accueil'
import BootstrapVue from 'bootstrap-vue'
import Navbar from '@/components/ElementCommun/Navbar'
import Profil from '@/components/Profil'
import Boutique from '@/components/Boutique'
import Presentation from '@/components/BattleJearceBody/Presentation'
import Inscription from '@/components/BattleJearceBody/Inscription'
import Connexion from '@/components/BattleJearceBody/Connexion'
import Reglement from '@/components/BattleJearceBody/Reglement'
import Contact from '@/components/BattleJearceBody/Contact'
import Validation from '@/components/Validation'
import Formation from '@/components/Formation'
import StatProfil from '@/components/ProfilBody/StatsProfil'
import InventairePersonnages from '@/components/ProfilBody/inventairePersonnages'
import InventaireObjets from '@/components/ProfilBody/inventaireObjets'
import NavBarProfil from '@/components/ProfilBody/NavBarProfil'
import NavBarBoutique from '@/components/BoutiqueBody/NavBarBoutique'
import BoutiquePersonnages from '@/components/BoutiqueBody/BoutiquePersonnages'
import BoutiqueObjets from '@/components/BoutiqueBody/BoutiqueObjets'
import NavBarFormation from '@/components/FormationBody/NavBarFormation'
import ListeFormation from '@/components/FormationBody/ListeFormation'
import NewFormation from '@/components/FormationBody/CreationFormation'

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
      path: '/Validation',
      name: 'Validation',
      component: Validation,
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
        }
      ]
    },
    {
      path: '/Profil',
      name: 'Profil',
      component: Profil,
      children: [
        {
          path: 'Stats',
          components: {
            default: Header,
            body: StatProfil,
            navBar: Navbar,
            navBarP: NavBarProfil
          }
        },
        {
          path: 'InventairePersonnages',
          components: {
            default: Header,
            body: InventairePersonnages,
            navBar: Navbar,
            navBarP: NavBarProfil
          }
        },
        {
          path: 'InventaireObjets',
          components: {
            default: Header,
            body: InventaireObjets,
            navBar: Navbar,
            navBarP: NavBarProfil
          }
        }
      ]
    },
    {
      path: '/Boutique',
      name: 'Boutique',
      component: Boutique,
      children: [
        {
          path: 'BoutiquePersonnages',
          components: {
            default: Header,
            body: BoutiquePersonnages,
            navBar: Navbar,
            navBarB: NavBarProfil
          }
        },
        {
          path: 'BoutiqueObjets',
          components: {
            default: Header,
            body: BoutiqueObjets,
            navBar: Navbar,
            navBarB: NavBarProfil
          }
        }
      ]
    },
    {
      path: '/Formation',
      name: 'Formation',
      component: Formation,
      children: [
        {
          path: 'Liste',
          components: {
            default: Header,
            body: ListeFormation,
            navBar: Navbar,
            navBarF: NavBarFormation
          }
        },
        {
          path: 'New',
          components: {
            default: Header,
            body: NewFormation,
            navBar: Navbar,
            navBarF: NavBarFormation
          }
        }
      ]
    }
  ]
})
