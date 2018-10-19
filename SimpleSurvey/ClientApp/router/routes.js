import HomePage from 'components/home-page'
import Survey from 'components/survey'
import SurveyResults from 'components/survey-results'

export const routes = [
  { name: 'home', path: '/', component: HomePage, display: 'Home', icon: 'home' },
  { name: 'survey', path: '/survey', component: Survey, display: 'Survey', icon: 'list' },
  { name: 'survey-results', path: '/survey-results', component: SurveyResults, display: 'Survey Results', icon: 'list' }
]
