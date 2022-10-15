import {
	sequence,
	trigger,
	animate,
	style,
	group,
	query,
	transition,
	animateChild,
	state,
	animation,
	useAnimation,
	stagger
} from '@angular/animations';

const reusable = animation([
	style({
		opacity: '{{opacity}}',
		transform: 'scale({{scale}}) translate3d({{x}}, {{y}}, {{z}})'
	}),
	animate('{{duration}} {{delay}} cubic-bezier(0.0, 0.0, 0.2, 1)', style('*'))
], {
	params: {
		duration: '200ms',
		delay: '0ms',
		opacity: '0',
		scale: '1',
		x: '0',
		y: '0',
		z: '0'
	}
});

export const SharedAnimations = [
	trigger('animate', [transition('void => *', [useAnimation(reusable)])]),

	trigger('fadeInOut', [
		state('0', style({
			opacity: 0,
			display: 'none'
		})),
		state('1', style({
			opacity: 1,
			display: 'block'
		})),
		transition('0 => 1', animate('300ms')),
		transition('1 => 0', animate('300ms'))
	]),
];

export const Slide = [
	trigger('slide', [
		state('left', style({ transform: 'translateX(0)' })),
		state('right', style({ transform: 'translateX(120%)' })),
		transition('* => *', animate(300))
	])
];


export const InOut = [
	trigger(
		'inOut',
		[
			transition(
				':enter',
				[
					style({ height: 0, opacity: 0 }),
					animate('1s ease-out',
						style({ height: 300, opacity: 1 }))
				]
			),
			transition(
				':leave',
				[
					style({ height: 300, opacity: 1 }),
					animate('1s ease-in',
						style({ height: 0, opacity: 0 }))
				]
			)
		]
	)

];

export const Fade = [
	trigger('fade', [
		transition('void => *', [
			style({ opacity: 0 }),
			animate(500, style({ opacity: 1 }))
		])
	])
]
